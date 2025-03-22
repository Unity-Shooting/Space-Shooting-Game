using UnityEngine;

public class MonsterSupport : Monster
{
    [SerializeField] private GameObject Launcher;
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
        IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet.Init(Launcher.transform.position, Vector2.down, 0);
    }
}
