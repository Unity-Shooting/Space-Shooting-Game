using UnityEngine;

public class MonsterSC : Monster
{
    [SerializeField] private GameObject Launcher;
    [SerializeField] private float curveRate;
    protected override void OnEnable()  // Start()랑 같은 역할 한다고 보시면 됩니다
    {
        base.OnEnable();    // 상속받은 Monster 클래스의 OnEnable() 실행. 공통적인 변수들 초기화는 저기서 했음

        InvokeRepeating("Shoot", AttackStart, AttackSpeed);
    }
    void Update()
    {
        Turn();
        Move();
    }

    public override void Shoot()
    {
        // 플레이어 위치로 MbBullet 발사

        // 몬스터에서 플레이어를 향하는 단위벡터
        Vector2 toPlayer = (PlayerController.Instance.transform.position - this.transform.position).normalized;



        IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet.Init(Launcher.transform.position, toPlayer, 0);
    }

    private void Turn()
    {
        float curve_direction = 1 ;

        if (type == 0) // 패턴 0이면 직선
        {
            return;
        }
        else if (type == 1) // 1이면 반시계방향 턴
        {
            curve_direction = 1;
        }
        else if (type == 2) // 2면 시계방향 턴
        {
            curve_direction = -1;
        }

        Vector2 tangent = new Vector2(-direction.y, direction.x); // 탄젠트벡터 (진행 방향의 왼쪽 수직)
        direction += curve_direction * curveRate * Time.deltaTime * tangent;  // 진행방향 벡터에 왼쪽방향 수직 벡터를 더해서 방향변경
        direction.Normalize();
        RotateToDirection(); // 바뀐 진행방향에 맞춰 스프라이트 회전
    }
}
