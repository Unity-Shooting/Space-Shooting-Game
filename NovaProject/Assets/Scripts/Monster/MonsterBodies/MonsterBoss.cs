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

    private enum BossPhase { Phase1, Phase2 }
    private BossPhase bossPhase = BossPhase.Phase1;

    [SerializeField] private float stopDelay;
    [SerializeField] private float stopDuration;
    [SerializeField] private GameObject mbRay;
    bool canBeDamaged = false;
    [SerializeField] private GameObject Launcher;
    [SerializeField] private GameObject Launcher2;
    [SerializeField] private GameObject Launcher3;
    [SerializeField] private GameObject Shield;
    private Animator animator;

    [SerializeField] private SpawnTimelineSO WavePatternA; // 일정 주기로 가로로 지나가는 Fighter 편대 소환
    [SerializeField] private SpawnTimelineSO WavePatternB; // 체력 50퍼 진입시 support 6마리 소환 바닥패턴
    [SerializeField] private SpawnTimelineSO WavePatternC; // 체력 50퍼센트 이하 패턴, 일정 주기로 패턴 101의 Bomber 소환

    private Coroutine PatternA; // 일정 주기로 spin 원형 뿌리기
    private Coroutine PatternB; // 등장시 레이저 한번 쏘고 일정주기로 레이저 쏘기
    private Coroutine PatternC; // 일정 주기로 가로로 지나가는 Fighter 편대 소환
    private Coroutine PatternD; // 체력 50퍼센트 이하 패턴, 양쪽 날개에서 spin 3연발
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
        if (bossPhase == BossPhase.Phase1 && HP < BaseHP / 2)
        {
            bossPhase = BossPhase.Phase2;
            StartCoroutine(SwitchPhase1toPhase2());
        }

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
        StartPhase1();
    }

    private IEnumerator SwitchPhase1toPhase2()
    {
        bossPhase = BossPhase.Phase2;
        StopPhase1();
        canBeDamaged = false; // 무적상태로 사이페 진행
        yield return StartCoroutine(BossMidPhasePattern());
        canBeDamaged = true; // 무적상태 끝
        StartPhase2();
    }

    private void StartPhase1()
    {
        canBeDamaged = true;
        PatternA = StartCoroutine(BossPatternA());
        PatternB = StartCoroutine(BossPatternB());
        PatternC = StartCoroutine(BossPatternC());
    }

    // 페이즈1 코루틴 정지
    private void StopPhase1()
    {
        if (PatternA != null)
        {
            StopCoroutine(PatternA);
        }
        if (PatternB != null)
        {
            StopCoroutine(PatternB);
        }
        if (PatternC != null)
        {
            StopCoroutine(PatternC);
        }
    }

    private void StartPhase2()
    {
        PatternA = StartCoroutine(BossPatternA());
        PatternD = StartCoroutine(BossPatternD());
        PatternE = StartCoroutine(BossPatternE());
    }

    private void StopPhase2()
    {
        if (PatternA != null)
        {
            StopCoroutine(PatternA);
        }
        if (PatternD != null)
        {
            StopCoroutine(PatternD);
        }
        if (PatternE != null)
        {
            StopCoroutine(PatternE);
        }
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
        yield return new WaitForSeconds(5);
        while (true)
        {
            StartCoroutine(SpawnManager.Instance.SpawnWave(WavePatternA));
            yield return new WaitForSeconds(periodC);
        }
    }

    IEnumerator BossMidPhasePattern()
    {
        //폭발 이펙트 재생
        var DE = PoolManager.instance.Get(DesturctionEffect);
        DE.transform.position = transform.position;
        DE.transform.rotation = transform.rotation;
        DE.SetActive(true);
        yield return new WaitForSeconds(1);

        //쉴드 효과 켜기
        Shield.SetActive(true);

        yield return new WaitForSeconds(3);
        yield return StartCoroutine(SpawnManager.Instance.SpawnWave(WavePatternB));
        for (int i = 0; i < 7; i++)
        {
            CircleFire();
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(2);
        // 실드 효과 끄기
        Shield.SetActive(false);
    }

    /// <summary>
    /// 패턴D : 체력 50퍼 이하 패턴, 양쪽 날개에서 spin 3연발
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternD()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(BurstFire());
        }
    }

    IEnumerator BurstFire()
    {
        int bulletCount = 3; // 양쪽에서 쏘니까 3발
        float fireInterval = 0.35f;
        float spreadAngle = 7f;

        Vector2 vectorToPlayer2 = PlayerController.Instance.transform.position - Launcher2.transform.position;
        Vector2 vectorToPlayer3 = PlayerController.Instance.transform.position - Launcher3.transform.position;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 fireDirection2 = Quaternion.Euler(0, 0, Random.Range(-spreadAngle, spreadAngle)) * vectorToPlayer2; // 흩뿌리기
            Vector2 fireDirection3 = Quaternion.Euler(0, 0, Random.Range(-spreadAngle, spreadAngle)) * vectorToPlayer3; // 흩뿌리기


            IBulletInit bul = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bul.Init(Launcher2.transform.position, fireDirection2, 0);
            IBulletInit bul2 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bul2.Init(Launcher3.transform.position, fireDirection3, 0);
            yield return new WaitForSeconds(fireInterval);
        }

    }

    /// <summary>
    /// 패턴E : 체력 50퍼 이하 패턴, 일정 주기로 패턴 101의 Bomber 소환
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternE()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            yield return StartCoroutine(SpawnManager.Instance.SpawnWave(WavePatternC));
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

    protected override void Die()
    {
        StopPhase1();
        StopPhase2();
        Debug.Log("Enter Destruction Process");
        ScoreManager.instance.AddScore(Score);
        Destroy(gameObject);

        // 파괴 애니메이션 재생
        var DE = PoolManager.instance.Get(DesturctionEffect);
        DE.transform.position = transform.position;
        DE.transform.rotation = transform.rotation;
        DE.SetActive(true);

        GameManager.Instance.StartCoroutine(GameManager.Instance.ShowClearStageOneStart()); // StageOne Clear

    }
}
