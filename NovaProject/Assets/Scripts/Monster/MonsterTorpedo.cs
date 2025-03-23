using UnityEngine;

public class MonsterTorpedo : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    protected override void OnEnable()  // Start()�� ���� ���� �Ѵٰ� ���ø� �˴ϴ�
    {
        base.OnEnable();    // ��ӹ��� Monster Ŭ������ OnEnable() ����. �������� ������ �ʱ�ȭ�� ���⼭ ����

        InvokeRepeating("Shoot", AttackStart, AttackSpeed);
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet1.Init(Launcher1.transform.position, direction, 0);
        IBulletInit bullet2 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet2.Init(Launcher2.transform.position, direction, 0);
    }
}
