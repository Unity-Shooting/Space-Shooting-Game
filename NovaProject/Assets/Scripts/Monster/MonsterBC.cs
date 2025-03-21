using System.Collections;
using UnityEngine;

public class MonsterBC : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    int ShootingShakeFlag = 1;
    protected override void OnEnable()  // Start()�� ���� ���� �Ѵٰ� ���ø� �˴ϴ�
    {
        base.OnEnable();    // ��ӹ��� Monster Ŭ������ OnEnable() ����. �������� ������ �ʱ�ȭ�� ���⼭ ����

        InvokeRepeating("Shoot", 1, 1);
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        // BC�� ��ä��ź���� ������ �ٲ㰡�鼭 �Ѹ�����
        int count = 5; // 5�� �߻�
        float angle = 60f; // ��ä�� ����
        float intervalangle = angle / (count - 1); // �� źȯ ������ ����
        float baseangle = -angle / 2f; // ���� ���� źȯ�� ����
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
