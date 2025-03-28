using System.Collections;
using UnityEngine;


/// <summary>
/// 보스는 한번만 나오면 되니까 오브젝트풀링 x Instantiate로 생성
/// </summary>
public class Teacher : Monster
{

    private enum BossPhase { Phase1, Phase2 }

    bool canBeDamaged = false;
    [SerializeField] private GameObject Launcher; // 입
    [SerializeField] private GameObject Launcher2; // 손
    [SerializeField] private SpriteRenderer Shadow; // 검은 그림자 렌더러

    [SerializeField] private GameObject unityBullet; // 유니티 총알
    [SerializeField] private GameObject indiBullet; // 인디코드 총알
    [SerializeField] private GameObject lionBullet; // 멋사 총알
    [SerializeField] private GameObject textBullet; // 텍스트 총알




    // 페이즈전환/사망시 코루틴 정지를 위한 변수
    private Coroutine PatternA; // 패턴A 1초마다 유니티총알을 플레이어에게 유도
    private Coroutine PatternB; // 패턴B 3초마다 텍스트 총알을 플레이어방향으로 발사


    void Start()
    {
        canBeDamaged = false;
        direction = Vector2.zero;
        transform.position = new Vector2(0, 5);
        
        // 검은 실루엣이 서서히 강사님으로 바뀌는 효과

        //페이즈 시작
        StartPhase();
    }

    /// <summary>
    /// Phase 시작
    /// </summary>
    private void StartPhase()
    {
        canBeDamaged = true;
        direction = Vector2.left;
        PatternA = StartCoroutine(BossPatternA());
        PatternB = StartCoroutine(BossPatternB());
    }

    // 페이즈1 코루틴 정지
    private void StopPhase()
    {
        if (PatternA != null)
        {
            StopCoroutine(PatternA);
        }
        if (PatternB != null)
        {
            StopCoroutine(PatternB);
        }
    }

    IEnumerator BossPatternA()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // 유니티 총알을 플레이어에게 유도
            IBulletInit bul = PoolManager.instance.Get(unityBullet).GetComponent<IBulletInit>();
            bul.Init(Launcher2.transform.position, Vector2.down, 0);
        }
    }

    IEnumerator BossPatternB()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            // 텍스트 총알을 플레이어 방향으로 발사

            Instantiate(textBullet, Launcher.transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        Move();
        TurnOnBothSide();
    }

    public override void Shoot()
    {
        
    }

    protected override void Die()
    {
        StopPhase();
        ScoreManager.instance.AddScore(Score);
        Destroy(gameObject);

        // 엔딩!
    }

    /// <summary>
    ///  양 사이드 가면 턴해서 돌아오기
    ///  턴하면서 인디코드/멋사 총알 뿌리기
    /// </summary>
    private void TurnOnBothSide()
    {
        float SideX = 2f;
        if (transform.position.x < -SideX)
        {   // 왼쪽 벽에 도착했을 때
            // 방향이 왼쪽이라면
            if(direction.x < 0)
            {
                direction = new Vector2(1, 0); //방향 전환
                //인디코드 총알 뿌리기
                CircleFire(indiBullet);
            }
        }
        else if (transform.position.x > SideX)
        {   // 오른쪽 벽에 도착했을 때
            // 방향이 오른쪽 이라면
            if (direction.x > 0)
            {
                direction = new Vector2(-1, 0); // 방향 전환
                //멋사총알 뿌리기 
                CircleFire(lionBullet);
            }
        }
    }


    private void CircleFire(GameObject bullet)
    {
        float angle = 360f;  // 발사각, 원형이니까 360
        int bulletcount = 15; // 발사할 총알의 숫자

        float angle2 = angle / (bulletcount - 1);  // 각 탄막 사이의 각도
        float baseAngle = Time.time * 20f; // 각 발사마다 회전하면서 발사하도록 베이스각도를 돌리기
        for (int i = 0; i < bulletcount; i++)
        {
            float shootangle = baseAngle + angle2 * i;
            Vector2 shootdir = Quaternion.Euler(0, 0, shootangle) * Vector2.down; // 발사할 방향
            IBulletInit bul = PoolManager.instance.Get(bullet).GetComponent<IBulletInit>();
            bul.Init(transform.position, shootdir, 0);
        }
    }
}
