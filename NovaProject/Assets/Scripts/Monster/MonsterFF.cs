using UnityEngine;
using UnityEngine.WSA;

public class MonsterFF : Monster
{
    [SerializeField] private GameObject Launcher;
    protected override void OnEnable()  // Start()랑 같은 역할 한다고 보시면 됩니다
    {
        base.OnEnable();    // 상속받은 Monster 클래스의 OnEnable() 실행. 공통적인 변수들 초기화는 저기서 했음

        InvokeRepeating("Shoot", 1, 1);
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        int count = 3; // 5발 발사
        float angle = 60f; // 부채꼴 각도
        float intervalangle = angle / (count - 1); // 각 탄환 사이의 각도
        float baseangle = -angle / 2f; // 제일 왼쪽 탄환의 각도


        for (int i = 0; i < count; i++)
        {
            float bulletangle = baseangle + intervalangle * i;

            Vector2 shootdir = Quaternion.Euler(0, 0, bulletangle) * Vector2.down;


            IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet1.Init(Launcher.transform.position, shootdir, 0);


        }
    }
}
