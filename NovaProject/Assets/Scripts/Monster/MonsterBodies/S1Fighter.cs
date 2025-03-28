using System.Collections;
using UnityEngine;

public class S1Fighter : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    [SerializeField] private float stopDuration;
    Coroutine firesequence;
    private Animator ani;

    protected override void StartAfterInit()
    {
        if (type == 1) // 보스전 전용 패턴
        {
            MoveSpeed = 3;
        }
        firesequence = StartCoroutine(FireSequence());
        StartCoroutine(StopDuringDuration(type, stopDuration));
    }

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }


    IEnumerator FireSequence()
    {
        yield return new WaitForSeconds(AttackStart);

        while (true)
        {
            ani.SetTrigger(nameof(Fire));
            Debug.Log($"Trigger has set");
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    IEnumerator Fire()
    {
        int bulletCount = 3; // 양쪽에서 쏘기떄문에 2배
        float fireInterval = 0.35f;
        float spreadAngle = 3f;
        
        Vector2 vectorToPlayer = PlayerController.Instance.transform.position - transform.position;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 fireDirection = Quaternion.Euler(0, 0, Random.Range(-spreadAngle, spreadAngle)) * vectorToPlayer; // 흩뿌리기

            //왼쪽 한발
            IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet.Init(Launcher2.transform.position, fireDirection, 0);
            yield return new WaitForSeconds(fireInterval);
            // 오른쪽 한발
            IBulletInit bullet2 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet2.Init(Launcher1.transform.position, fireDirection, 0);
            yield return new WaitForSeconds(fireInterval);

        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (firesequence != null)
            StopCoroutine(firesequence);
    }


    /// <summary>
    /// 보스전애 소환되는 몬스터 전용 정지 코루틴
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    protected IEnumerator StopDuringDuration(float delay, float duration)
    {
        Debug.Log($"delay : {delay}");
        // 몬스터 생성시 type를 정지까지 지연시간으로 사용할건데 0이면 정지하지 않는 패턴
        // 0이면 멈추지 않고 계속 가도록 코루틴 중지! 10이상으로 줄 일은 없을테니
        // 추가 패턴이 필요한 경우 11 등으로 줄 수 있게 10이상이어도 정지하지 않음
        if (delay == 0 || delay >= 10)
        {
            yield break;
        }

        Debug.Log("Enterd Stop");

        yield return new WaitForSeconds(delay);
        float time = 0;
        float initMoveSpeed = MoveSpeed;
        while (time < duration)
        {
            time += Time.deltaTime;
            MoveSpeed = Mathf.Lerp(initMoveSpeed, 0f, time / duration); // 지난 시간에 따라 속도를 초기속도~0으로 보간
            yield return null;
        }

        MoveSpeed = 0f; // 시간이 지난 후에 완전히 정지하도록 보장
    }



    void Update()
    {
        Move();
    }

    public override void Shoot()
    {

    }
}
