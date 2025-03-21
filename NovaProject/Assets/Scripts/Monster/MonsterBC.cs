using System.Collections;
using UnityEngine;

public class MonsterBC : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    int ShootingShakeFlag = 1;
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
        // BC는 부채꼴탄막을 각도를 바꿔가면서 뿌릴것임
        int count = 5; // 5발 발사
        float angle = 60f; // 부채꼴 각도
        float intervalangle = angle / (count - 1); // 각 탄환 사이의 각도
        float baseangle = -angle / 2f; // 제일 왼쪽 탄환의 각도
        float shake = 5f * ShootingShakeFlag;

        for (int i = 0; i < count; i++)
        {
            float bulletangle = baseangle + intervalangle * i + shake;

            Vector2 shootdir = Quaternion.Euler(0, 0, bulletangle) * Vector2.down;
            if (ShootingShakeFlag == 1)
            {
                IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
                bullet1.Init(Launcher1.transform.position, shootdir, 0);
            }
            else
            {
                IBulletInit bullet2 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
                bullet2.Init(Launcher2.transform.position, shootdir, 0);
            }
        }

        ShootingShakeFlag *= -1;
    }







}
