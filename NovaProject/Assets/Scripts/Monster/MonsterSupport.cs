using UnityEngine;

public class MonsterSupport : Monster
{
    [SerializeField] private GameObject Launcher;
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
        IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet.Init(Launcher.transform.position, Vector2.down, 0);
    }
}
