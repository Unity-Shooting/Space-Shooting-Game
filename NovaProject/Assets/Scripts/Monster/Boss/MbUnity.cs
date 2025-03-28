using UnityEngine;

public class MbUnity : MbBase
{
    
    [SerializeField] private float hoamingPower;
    [SerializeField] private float maxHoamingAngle;

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
        RotateToDirection();

    }

}
