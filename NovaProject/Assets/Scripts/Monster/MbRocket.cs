using UnityEngine;
using UnityEngine.WSA;

public class MbRocket : MbBase, IDamageable
{
    [SerializeField] private float hoamingPower;
    [SerializeField] private float maxHoamingAngle;
    [SerializeField] private float maxHP;
    private float HP;

    void Update()
    {
        Hoaming();
        Move();
    }
    /// <summary>
    /// �÷��̾� �������� �����ϴ� �Լ�
    /// �Ѿ��� ���� vector2 direction�� �����ؼ� ���� ������ �ٲٰ�
    /// �̹����� �ش� ������ ȸ�����Ѽ� �ڿ������� �����Ҽ��ֵ��� �õ��غ�
    /// ������ ��� �÷��̾� �Ѿ˷� ������ �� �ֵ��� ��
    /// �÷��̾ ����ģ �̻����� �ؿ��� �ö���� �ʵ��� direction�� ������ y����� �¿� maxDegree���� ����
    /// </summary>
    private void Hoaming()
    {
        // ���Ͽ��� �÷��̾ ���ϴ� ��������
        Vector2 toPlayer = (PlayerController.Instance.transform.position - this.transform.position).normalized;
        // �Ʒ����� �������Ϳ��� ������ maxDegree ������ ��� ����
        float angleToDown = Vector2.SignedAngle(Vector2.down, toPlayer);
        if(Mathf.Abs(angleToDown) < maxHoamingAngle)
        {
            direction = Vector2.Lerp(direction, toPlayer, hoamingPower * Time.deltaTime);
        }
        // �̻��� ��������Ʈ�� ȸ�������ֱ�
        // ��ü ��ü�� ȸ���ϴϱ� �� ȸ���� �̵������� ������� �ʵ��� Move�� World ��ǥ �������� ����!!
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // z��ȸ���ε� Quaternion.Euler�� �������� �ٶ󺸴� �̹��� �������� ȸ�������ִϱ� 90f ���ؼ� ����
        transform.rotation = Quaternion.Euler(0,0,rotationAngle+90f);

    }

    public void TakeDamage(int  damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            Release();
        }
    }
    protected override void OnEnable()
    {
        // �ٸ� �Ѿ˵��� ������ �ʴ� ü���� ������ ������ ���� �ʱ�ȭ
        base.OnEnable();
        HP = maxHP;
    }

}
