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
public class S1Boss : Monster
{

    private enum BossPhase { Phase1, Phase2 }
    private BossPhase bossPhase = BossPhase.Phase1;

    [SerializeField] private float stopDelay;
    [SerializeField] private float stopDuration;
    [SerializeField] private GameObject aWarning;
    [SerializeField] private GameObject s1MbRay;
    [SerializeField] private GameObject s1MbTorpedo;
    bool canBeDamaged = false;
    [SerializeField] private GameObject Launcher;
    [SerializeField] private GameObject Launcher2;
    [SerializeField] private GameObject Launcher3;
    [SerializeField] private GameObject Shield;
    private Animator animator;

    [SerializeField] private SpawnTimelineSO WavePatternA; // 일정 주기로 보스위에 멈춰서 사격하는 S1Fighter 소환

    // 페이즈전환/사망시 코루틴 정지를 위한 변수
    private Coroutine PatternA;  
    private Coroutine PatternB;  
    private Coroutine PatternC; 
    private Coroutine PatternD; 
    private Coroutine PatternE; 


    void Start()
    {
        canBeDamaged = false;
        direction = Vector2.down;
        transform.position = new Vector2(0, 5);
        animator = GetComponent<Animator>();
        StartCoroutine(StopDuringDuration()); // 등장한 후 서서히 정지 후 패턴 시작
    }

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
        PatternB = StartCoroutine(BossPatternB());
        PatternD = StartCoroutine(BossPatternD());
        PatternE = StartCoroutine(BossPatternE());
    }

    private void StopPhase2()
    {
        if(PatternB != null)
        {
            StopCoroutine(PatternB);
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
    /// 패턴A : 4발의 유도 Torpedo 발사
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternA()
    {
        int bulletCount = 4; // 양쪽에서 발사하니까 2배나감
        float periodA = 4f;
        while (true)
        {
            yield return new WaitForSeconds(periodA);
            // 양쪽으로 펼쳐지는 유도미사일 탄막
            Vector2 dirDownLeft = new Vector2(-0.5f, -1).normalized;
            float spreadTotal = 70f;
            float spreadAngle = spreadTotal / (bulletCount - 1);
            for (int i = 0; i < bulletCount; i++)
            {
                float fireAngle = -spreadTotal + spreadAngle * i;
                Vector2 fireDir = Quaternion.Euler(0, 0, fireAngle) * dirDownLeft;
                FireBullet(s1MbTorpedo, Launcher2.transform.position, fireDir, 1);
                FireBullet(s1MbTorpedo, Launcher3.transform.position, FlipX(fireDir), 1);
            }

        }

    }



    /// <summary>
    /// 패턴B : 일정 주기로 플레이어를 향해 레이저 발사
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternB()
    {
        float periodB = 5;
        while (true)
        {
            animator.SetTrigger("Attack");
            Vector2 toPlayer = (Vector2)PlayerController.Instance.transform.position - (Vector2)Launcher.transform.position;
            S1MbRay ray2 = PoolManager.instance.Get(s1MbRay).GetComponent<S1MbRay>();
            ray2.Init(Launcher.transform.position, toPlayer, 1);


            yield return new WaitForSeconds(periodB);
        }
    }

    /// <summary>
    /// 랜덤방향에서 화면 중앙 근처를 향해 화살표경고 후 레이저
    /// 간격이 긴 2연사
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternC()
    {
        float periodC = 6;
        float interval = 1f;
        int count = 2;
        while (true)
        {
            yield return new WaitForSeconds(periodC);
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(interval);
                RandomArrowLaseer();
            }
        }
        
    }

    /// <summary>
    /// 화살표 경고 표시 후 지속시간 짧은 레이저 발사
    /// 화면가장자리 랜덤으로 시작해서 화면 중앙 근처로 레이저 발사
    /// </summary>
    /// <returns></returns>
    private void RandomArrowLaseer()
    {
        Vector2 pos = GetRandomPointOnBorder(); // 화면 가장자리 랜덤위치
        Vector2 dir = new Vector2(0,-2) - pos; //화면 중간하단으로 향하는 방향
        float spreadMax = 20f;
        float spread = Random.Range(-spreadMax, spreadMax);
        dir = Quaternion.Euler(0, 0, spread) * dir;
        StartCoroutine(ArrowLaser(pos, dir));
    }

    IEnumerator ArrowLaser(Vector2 pos, Vector2 dir)
    {
        ArrowWarning arrow = PoolManager.instance.Get(aWarning).GetComponent<ArrowWarning>();
        arrow.Init(pos, dir);

        yield return new WaitForSeconds(3.2f);
        S1MbRay ray = PoolManager.instance.Get(s1MbRay).GetComponent<S1MbRay>();
        ray.Init(pos, dir, 2);
    }

    IEnumerator BossMidPhasePattern()
    {

        yield return new WaitForSeconds(1);

        //쉴드 효과 켜기
        Shield.SetActive(true);

        yield return new WaitForSeconds(3);

        yield return StartCoroutine(MidPhase());
        
        yield return new WaitForSeconds(2);
        // 실드 효과 끄기
        Shield.SetActive(false);
    }

    /// <summary>
    /// 중간페이즈 무적 패턴
    /// 레이저 아무튼 많이 날림 
    /// </summary>
    /// <returns></returns>
    IEnumerator MidPhase()
    {
        yield return new WaitForSeconds(1);

        //함수로 분리해도 하드코딩 하는건 똑같을 것 같아서 나누지 않음
        float intervalBurst = 1.5f;
        float intervalLaser = 0.1f;

        //1차 공격
        StartCoroutine(ArrowLaser(new Vector2(-4.91f, 5.57f), new Vector2(0.728f, -0.685f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-2.57f, 6.09f), new Vector2(0.253f, -0.967f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(4.89f, 5.2f), new Vector2(-0.558f, -0.829f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-5.24f, -4.33f), new Vector2(0.906f, 0.422f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-5.85f, -0.98f), new Vector2(0.994f, -0.105f)));

        yield return new WaitForSeconds(intervalBurst);
        //2차 공격
        StartCoroutine(ArrowLaser(new Vector2(5.08f, 1.86f), new Vector2(-0.524f, -0.851f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-4.85f, 2.26f), new Vector2(0.746f, -0.665f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-5.28f, -3.29f), new Vector2(0.915f, 0.401f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(1.85f, -6.12f), new Vector2(-0.106f, 0.994f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-3.72f, 5.84f), new Vector2(0.646f, -0.763f)));

        yield return new WaitForSeconds(intervalBurst);
        //3차 공격
        StartCoroutine(ArrowLaser(new Vector2(4.04f, -5.58f), new Vector2(-0.928f, 0.370f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-5.68f, 6.8f), new Vector2(0.466f, -0.884f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-5.36f, -6.03f), new Vector2(0.884f, 0.466f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(6.24f, 2.05f), new Vector2(-0.949f, -0.313f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-3.32f, -5.47f), new Vector2(0.313f, 0.949f)));
        yield return new WaitForSeconds(intervalLaser);
        StartCoroutine(ArrowLaser(new Vector2(-1.41f, 6.41f), new Vector2(0.300f, -0.953f)));

        yield return new WaitForSeconds(2);
    }

    /// <summary>
    /// 패턴D : 체력 50퍼 이하 패턴, 패턴A보다 더 많은 로켓
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternD()
    {
        int bulletCount = 7; // 양쪽에서 발사하니까 2배나감
        float periodA = 4f;
        while (true)
        {
            yield return new WaitForSeconds(periodA);
            // 양쪽으로 펼쳐지는 유도미사일 탄막
            Vector2 dirDownLeft = new Vector2(-0.5f, -1).normalized;
            float spreadTotal = 70f;
            float spreadAngle = spreadTotal / (bulletCount - 1);
            for (int i = 0; i < bulletCount; i++)
            {
                float fireAngle = -spreadTotal + spreadAngle * i;
                Vector2 fireDir = Quaternion.Euler(0, 0, fireAngle) * dirDownLeft;
                FireBullet(s1MbTorpedo, Launcher2.transform.position, fireDir, 1);
                FireBullet(s1MbTorpedo, Launcher3.transform.position, FlipX(fireDir), 1);
            }

        }
    }


    /// <summary>
    /// 패턴E : 체력 50퍼 이하 패턴, 패턴 C보다 짧은간격의 4연사 레이저
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPatternE()
    {
        float periodC = 6;
        float interval = 0.5f;
        int count = 4;
        while (true)
        {
            yield return new WaitForSeconds(periodC);
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(interval);
                RandomArrowLaseer();
            }
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

        // 다음 스태이지로 넘어가기
    }

    private Vector2 FlipX(Vector2 vector)
    {
        return new Vector2(-vector.x, vector.y);
    }

    /// <summary>
    /// 화면 가장자리중 랜덤 포인트 
    /// </summary>
    /// <returns></returns>
    private Vector2 GetRandomPointOnBorder()
    {
        float xMin = -5.2f;
        float xMax = 5.2f;
        float yMin = -6.2f;
        float yMax = 6.2f;

        int edge = Random.Range(0, 4); // 0: 위, 1: 아래, 2: 왼쪽, 3: 오른쪽
        float x = 0f;
        float y = 0f;

        switch (edge)
        {
            case 0: // 위쪽 테두리
                x = Random.Range(xMin, xMax);
                y = yMax;
                break;
            case 1: // 아래쪽 테두리
                x = Random.Range(xMin, xMax);
                y = yMin;
                break;
            case 2: // 왼쪽 테두리
                x = xMin;
                y = Random.Range(yMin, yMax);
                break;
            case 3: // 오른쪽 테두리
                x = xMax;
                y = Random.Range(yMin, yMax);
                break;
        }

        return new Vector2(x, y);
    }
}
