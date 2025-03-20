using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class MbBase : MonoBehaviour
{
    protected bool isReleased = false; // �ߺ���ȯ ������ �÷���
    public Vector2 direction { get; protected set; }
    public int Attack {  get; protected set; }
    public float MoveSpeed { get; protected set; }

    [SerializeField] protected int BaseAttack;
    [SerializeField] protected float BaseMoveSpeed;

    public virtual void Move()
    {
        transform.Translate(MoveSpeed * direction * Time.deltaTime);
    }

    protected virtual void OnEnable() // �ʱ�ȭ Start() ��� ���
    {
        isReleased = false;
        Attack = BaseAttack;
        MoveSpeed = BaseMoveSpeed;
    }

    protected virtual void Release() // ������Ʈ Ǯ�� ���� 
    {
        if (!isReleased)
        {
            isReleased = true;
            PoolManager.instance.Return(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) // �÷��̾� �浹 �� �׼�
    {
        
    }
}
