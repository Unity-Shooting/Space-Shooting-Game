using UnityEngine;

// ��� ���͵��� ��� �� �߻�Ŭ����
// ���͸��� �̵��̳� ����� �ٸ��� �Ϸ��� 1945���� �ٸ��� ���ͺ��� Ŭ������ ���� ��� �� �� ���Ƽ�
// ���������� �ʿ��� �κ� 
// ���� � ���͸� ������� �����Ŵ������� Monster.abc() ���·� ��κ��� ����� ��� �� �� �ֵ��� �õ�
public abstract class Monster : MonoBehaviour
{
    protected bool isReleased = false;  // ������Ʈ Ǯ�� �� Release �Լ��� ȣ���ؼ� SetActive(false)�� ��Ȱ��ȭ �Ǹ�
                                        // OnBecameInvisible�� ���� ȣ��Ŵ� ������ �ذ��ϱ� ���� Ʈ����
                                        // OnDisable���� OnBecameInvisible�� ���� ����� �ִٰ� �ϴµ�
                                        // �ϴ� ���� ���α׷����� Disalbe�� �� �ʰ� ����ż� �ȵɰͰ��׿�
                                        // OnEnable���� false�� �ʱ�ȭ, Release() �Լ����� true�� ���� OnBecameInvisible���� Release�� ����
    public int HP { get; protected set; }  // �ִ� ü���̶� �����ص� ���� : ����� �� �ʱ�ȭ �ϱ� ����
    public Vector2 direction { get; protected set; } // �ʱ� ���� ����

    [SerializeField]  // �ν����Ϳ��� ������ ��ġ���� ��ũ��Ʈ���� �ʱⰪ �ȳֱ�(�򰥸�)
    protected int MaxHP;
    [SerializeField]
    protected int Attack; // ���ݷ�
    [SerializeField]
    protected float MoveSpeed; // �ӵ� 

    // ��ӹ��� Ŭ�������� ���� �� �޼����
    public abstract void Shoot();
    public abstract void Init();
    // �����Ŵ������� Get���� ������Ʈ ������ �������� �ݵ�� Init ���ֱ�
    public abstract void Init(Vector2 a, Vector2 b);
    public abstract void Die();

    public virtual void Move(Vector2 dir) // dir �������� speed �ӵ��� �̵�
    {
        transform.Translate(dir * MoveSpeed * Time.deltaTime);
    }

    public virtual void Damaged(int damage) // �������� ���� �� ü�� ���
    {
        HP -= damage;
        Debug.Log($"{this.name} damaged : {damage} HP : {HP}");
        if (HP < 0)  // ü�� 0�� �Ǹ� �����鼭 ���ͺ� ���� �� �׼� ����
        {
            Debug.Log($"{this.name} destroyed");
            Die();
        }
    }
    protected virtual void OnEnable()  // ������ƮǮ���� ������ �� Ȱ��ȭ(�ʱ�ȭ)
    {
        isReleased = false;
        HP = MaxHP;
        this.gameObject.GetComponent<Animator>().SetBool("Destroyed", false);
    }

    protected void Release()
    {
        Debug.Log("Release ȣ��");
        isReleased = true;
        PoolManager.instance.Return(gameObject);
    }

}
