using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class MbBase : MonoBehaviour, IBulletInit
{
    protected bool isReleased = false; // �ߺ���ȯ ������ �÷���
    public Vector2 direction { get; protected set; }
    public int Attack { get; protected set; }
    public float MoveSpeed { get; protected set; }

    [SerializeField] protected int BaseAttack;
    [SerializeField] protected float BaseMoveSpeed;

    public virtual void Init(Vector2 pos, Vector2 dir, int type)
    {
        transform.position = pos;
        direction = dir.normalized;

        RotateToDirection();
    }
    public virtual void Move()
    {
        transform.Translate(MoveSpeed * direction * Time.deltaTime, Space.World);  // �̵��� ������ǥ ���������ؼ� rotation ���� �ȹޱ�
    }

    protected virtual void OnEnable() // �ʱ�ȭ Start() ��� ���
    {
        transform.rotation = Quaternion.identity;
        isReleased = false;
        Attack = BaseAttack;
        MoveSpeed = BaseMoveSpeed;
        direction = Vector2.zero;
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
        ////�׽�Ʈ������ ���Ͷ� �浹
        //if (collision.gameObject.CompareTag("Monster"))
        //{
        //    IDamageable obj = collision.GetComponent<IDamageable>();
        //    obj.TakeDamage(1000);
        //    Release();
        //}

        if(collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(Attack);
            Release();
        }

    }

    protected virtual void OnBecameInvisible()
    {
        Release();
    }

    protected void RotateToDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // �̹����� �Ʒ������̴ϱ� 90�� ����
    }
}