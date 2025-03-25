using UnityEngine;

public class MonsterSC : Monster
{
    [SerializeField] private GameObject Launcher;
    [SerializeField] private float curveRate;
    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // ��� ����
        
    }
    void Update()
    {
        Turn();
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

    private void Turn()
    {
        float curve_direction = 1 ;

        if (type == 0) // ���� 0�̸� ����
        {
            return;
        }
        else if (type == 1) // 1�̸� �ݽð���� ��
        {
            curve_direction = 1;
        }
        else if (type == 2) // 2�� �ð���� ��
        {
            curve_direction = -1;
        }

        Vector2 tangent = new Vector2(-direction.y, direction.x); // ź��Ʈ���� (���� ������ ���� ����)
        direction += curve_direction * curveRate * Time.deltaTime * tangent;  // ������� ���Ϳ� ���ʹ��� ���� ���͸� ���ؼ� ���⺯��
        direction.Normalize();
        RotateToDirection(); // �ٲ� ������⿡ ���� ��������Ʈ ȸ��
    }
}
