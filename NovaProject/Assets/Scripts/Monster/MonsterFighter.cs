using UnityEngine;

public class MonsterFighter : Monster
{
    [SerializeField] private GameObject Launcher;
    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // ��� ����
        
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        // �÷��̾� ��ġ�� MbBullet �߻�

        // ���Ϳ��� �÷��̾ ���ϴ� ��������
        Vector2 toPlayer = (PlayerController.Instance.transform.position - this.transform.position).normalized;



        IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet.Init(Launcher.transform.position, toPlayer, 0);
    }
}
