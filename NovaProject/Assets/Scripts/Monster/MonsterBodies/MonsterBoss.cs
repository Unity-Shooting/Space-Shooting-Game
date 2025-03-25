using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

/// <summary>
/// 보스는 한번만 나오면 되니까 오브젝트풀링 x Instantiate로 생성
/// 1스테이지 보스는 MbSpin과 MbRay를 사용해서 공격하는걸로 하고
/// 중간중간 쫄 소환 패턴으로 다양한 공격을 시도
/// </summary>
public class MonsterBoss : Monster
{
    [SerializeField] private float stopDelay;
    [SerializeField] private float stopDuration;
    [SerializeField] private GameObject mbRay;
    bool canBeDamaged = false;
    [SerializeField] private GameObject Launcher;
    private Animator animator;

    [SerializeField] private SpawnTimelineSO WavePatternA; // 일정 주기로 가로로 지나가는 Fighter 편대 소환
    [SerializeField] private SpawnTimelineSO WavePatternB; // 체력 50퍼 진입시 support 6마리 소환 바닥패턴

    private Coroutine PatternA; // 일정 주기로 spin 원형 뿌리기
    private Coroutine PatternB; // 등장시 레이저 한번 쏘고 일정주기로 레이저 쏘기
    private Coroutine PatternC; // 일정 주기로 가로로 지나가는 Fighter 편대 소환
    private Coroutine PatternD; // 양쪽 날개에서 spin 3연발
    private Coroutine PatternE; // 체력 50퍼센트 이하 패턴, 일정 주기로 패턴 101의 Bomber 소환


    void Start()
    {
        direction = Vector2.down;
        transform.position = new Vector2(0, 5);
        animator = GetComponent<Animator>();
        StartCoroutine(StopDuringDuration()); // 등장한 후 서서히 정지 후 패턴 시작
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// 보스 전용 등장 함수
    /// 등장 후 stopDuration 동안 서서히 멈추고 패턴 시작
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    protected IEnumerator StopDuringDuration()
    {
        yield return new WaitForSeconds(stopDelay);
        float time = 0;
        float initMoveSpeed = MoveSpeed;
        while (time < stopDuration)
        {
            time += Time.deltaTime;
            MoveSpeed = Mathf.Lerp(initMoveSpeed, 0f, time / stopDuration); // 지난 시간에 따라 속도를 초기속도~0으로 보간
            yield return null;
        }
        // 확실하게 정지시키기
        MoveSpeed = 0f;

        // 감속이 끝나면 패턴 시작
        canBeDamaged = true;
        PatternA = StartCoroutine(BossPatternA());
        PatternB = StartCoroutine(BossPatternB());
        PatternC = StartCoroutine(BossPatternC());
    }

    /// <summary>
    /// 패턴A : 원형으로 MbSpin 발사
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternA()
    {
        float periodA = 2;
        while (true)
        {
            yield return new WaitForSeconds(periodA);
            CircleFire();
        }

    }

    /// <summary>
    /// 원형으로 탄막 뿌리기
    /// </summary>
    private void CircleFire()
    {
        float angle = 360f;  // 발사각, 원형이니까 360
        int bulletcount = 15; // 발사할 총알의 숫자

        float angle2 = angle / (bulletcount - 1);  // 각 탄막 사이의 각도
        float baseAngle = Time.time * 20f; // 각 발사마다 회전하면서 발사하도록 베이스각도를 돌리기
        for (int i = 0; i < bulletcount; i++)
        {
            float shootangle = baseAngle + angle2 * i;  
            Vector2 shootdir = Quaternion.Euler(0, 0, shootangle) * Vector2.down; // 발사할 방향
            IBulletInit bul = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bul.Init(transform.position, shootdir, 0);
        }
    }

    /// <summary>
    /// 패턴B : 일정 주기로 3갈래 레이저
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternB()
    {
        float periodB = 10;
        while (true)
        {
            animator.SetTrigger("Attack");
            //레이저는 방향만 돌려주면 알아서 작동
            float spreadAngle = 30f;
            MbRay ray = PoolManager.instance.Get(mbRay).GetComponent<MbRay>();
            ray.Init(Launcher.transform.position, Quaternion.Euler(0, 0, -spreadAngle) * Vector2.down, 1);
            MbRay ray2 = PoolManager.instance.Get(mbRay).GetComponent<MbRay>();
            ray2.Init(Launcher.transform.position, Vector2.down, 1);
            MbRay ray3 = PoolManager.instance.Get(mbRay).GetComponent<MbRay>();
            ray3.Init(Launcher.transform.position, Quaternion.Euler(0, 0, spreadAngle) * Vector2.down, 1);

            yield return new WaitForSeconds(periodB);
        }
    }

    /// <summary>
    /// 일정 주기로 가로로 지나가는 fighter 편대 소환
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternC()
    {
        float periodC = 10;
        while (true)
        {
            yield return new WaitForSeconds(periodC);
           StartCoroutine(SpawnManager.Instance.SpawnWave(WavePatternA));
        }
    }



    public override void Shoot()
    {

    }

    public override void TakeDamage(int damage)
    {
        if (canBeDamaged)
        {
            base.TakeDamage(damage);
        }
    }
}
