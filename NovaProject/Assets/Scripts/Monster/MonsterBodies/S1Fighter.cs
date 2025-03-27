using System.Collections;
using UnityEngine;

public class S1Fighter : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    Coroutine firesequence;
    private Animator ani;

    //protected override void StartAfterInit()
    //{

    //    firesequence = StartCoroutine(FireSequence());
    //}

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Start()  // 프리펩화 하기 전 하이어러키창에서 임시 테스트용
    {
        firesequence = StartCoroutine(FireSequence());
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


    void Update()
    {
        Move();
    }

    public override void Shoot()
    {

    }
}
