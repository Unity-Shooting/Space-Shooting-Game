using UnityEngine;

public abstract class Monster : MonoBehaviour
{

    private int HP;  // �ִ� ü���̶� �����ص� ���� : Init()�� �ʱ�ȭ �ϱ� ����

    [SerializeField]  // �ν����Ϳ��� ������ ��ġ���� ��ũ��Ʈ���� �ʱⰪ �ȳֱ�(�򰥸�)
    protected int MaxHP;
    [SerializeField]
    protected int Attack;
    [SerializeField]
    protected float MoveSpeed;

    public abstract void Shoot();
    public abstract void Move(Vector2 dir);

    public virtual void Damaged(int damage)
    {
        HP -= damage;
        Debug.Log($"{this.name} damaged : {damage} HP : {HP}");
        if (HP < 0)
        {
            Debug.Log($"{this.name} destroyed");
            Die();
        }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
        //������ƮǮ�� �ٲ㺸��
    }

}
