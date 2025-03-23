using UnityEngine;

public class MonsterSupport : Monster
{
    [SerializeField] public GameObject Launcher;
    protected override void OnEnable()  // Start()랑 같은 역할 한다고 보시면 됩니다
    {
        base.OnEnable();    // 상속받은 Monster 클래스의 OnEnable() 실행. 공통적인 변수들 초기화는 저기서 했음

        InvokeRepeating("Shoot", AttackStart, AttackSpeed);
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        //IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        //bullet.Init(Launcher.transform.position, Vector2.down, 0);
        // 이 몬스터는 레이저를 발사하는데 레이저를 중간에 멈추기 위해서 인수를 하나 더 넣어야함
        // IBulletInit로 범용 인터페이스 못쓰고 따로 레이저 클래스로 가져옴
        MbRay ray = PoolManager.instance.Get(Bullet).GetComponent<MbRay>();
        ray.Init(Launcher.transform.position, direction, this);
    }
}
