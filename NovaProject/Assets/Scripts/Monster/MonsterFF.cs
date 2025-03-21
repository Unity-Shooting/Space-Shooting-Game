using UnityEngine;
using UnityEngine.WSA;

public class MonsterFF : Monster
{
    [SerializeField] private GameObject Launcher;
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
        int count = 3; // 5�� �߻�
        float angle = 60f; // ��ä�� ����
        float intervalangle = angle / (count - 1); // �� źȯ ������ ����
        float baseangle = -angle / 2f; // ���� ���� źȯ�� ����


        for (int i = 0; i < count; i++)
        {
            float bulletangle = baseangle + intervalangle * i;

            Vector2 shootdir = Quaternion.Euler(0, 0, bulletangle) * Vector2.down;


            IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet1.Init(Launcher.transform.position, shootdir, 0);


        }
    }
}
