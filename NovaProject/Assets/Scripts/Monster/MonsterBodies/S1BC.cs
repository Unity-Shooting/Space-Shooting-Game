using System.Collections;
using UnityEngine;

public class S1BC : Monster
{
    [SerializeField] private GameObject Launcher;
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
        int bulletCount = 6;
        float fireInterval = 0.15f;
        float spreadAngle = 5f;
        Vector2 vectorToPlayer = PlayerController.Instance.transform.position - transform.position;

        for (int i = 0; i < bulletCount; i++)
        {
            Debug.Log("BurstFireStarted");
            Vector2 fireDirection = Quaternion.Euler(0, 0, Random.Range(-spreadAngle, spreadAngle)) * vectorToPlayer;
            IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet.Init(Launcher.transform.position, fireDirection, 0);
            yield return new WaitForSeconds(fireInterval);
            Debug.Log("afterInterval");
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
