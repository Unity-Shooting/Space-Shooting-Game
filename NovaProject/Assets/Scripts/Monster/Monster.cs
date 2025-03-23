using UnityEngine;

// ��� ���͵��� ��� �� �߻�Ŭ����
// ���͸��� �̵��̳� ����� �ٸ��� �Ϸ��� 1945���� �ٸ��� ���ͺ��� Ŭ������ ���� ��� �� �� ���Ƽ�
// ���������� �ʿ��� �κ� 
// ���� � ���͸� ������� �����Ŵ������� Monster.abc() ���·� ��κ��� ����� ��� �� �� �ֵ��� �õ�

/// <summary>
/// �� ���Ͱ� ���� ������ �� ������Ƽ
/// Gameobject Launcher
/// </summary>
public abstract class Monster : MonoBehaviour, IDamageable
{
    protected bool isReleased = false;  // ������Ʈ Ǯ�� �� Release �Լ��� ȣ���ؼ� SetActive(false)�� ��Ȱ��ȭ �Ǹ�
                                        // OnBecameInvisible�� ���� ȣ��Ŵ� ������ �ذ��ϱ� ���� Ʈ����
                                        // OnDisable���� OnBecameInvisible�� ���� ����� �ִٰ� �ϴµ�
                                        // �ϴ� ���� ���α׷����� Disalbe�� �� �ʰ� ����ż� �ȵɰͰ��׿�
                                        // OnEnable���� false�� �ʱ�ȭ, Release() �Լ����� true�� ���� OnBecameInvisible���� Release�� ����
    public int HP { get; protected set; }  // �ʱⰪ�̶� �����ص� ���� : ����� �� �ʱ�ȭ �ϱ� ����
    public int Attack { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttackStart { get; protected set; }
    public Vector2 direction { get; protected set; } // ���� ����
    public int type { get; protected set; }
    // �ν����Ϳ��� ������ ��ġ���� ��ũ��Ʈ���� �ʱⰪ �ȳֱ�(�򰥸�)
    [SerializeField] protected int BaseHP;
    [SerializeField] protected int BaseAttack; // ���ݷ�
    [SerializeField] protected float BaseMoveSpeed; // �ӵ� 
    [SerializeField] protected float BaseAttackSpeed;
    [SerializeField] protected float BaseAttackStart; // ù ���ݱ��� �����ð�
    [SerializeField] protected GameObject Bullet; // �߻��� �Ѿ� ������
    [SerializeField] protected int Score; // �߻��� �Ѿ� ������



    // ��ӹ��� Ŭ�������� ���� �� �޼����

    // ������ �ƴ� ���͵��� ��� ������ �������� ����� ������ �����̱⋚����
    // �ڷ�ƾ���� �κ�ũ�������� ����ؼ� Shoot�� �ҷ��� ����
    // ���� OnDisable()���� CancleInvoke ����
    public abstract void Shoot();


    public virtual void Die()
    {
        CancelInvoke("Shooting");
        this.gameObject.GetComponent<Animator>().SetTrigger("Destroy");  // �ı� �ִϸ��̼� ���
                                                                         // �ִϸ��̼� ����� Release() ȣ���ϵ��� �ִϸ��̼� Ŭ�� �����ص�
        DestroyAllChildren();  // �ı��ִϸ��̼� �����ϸ鼭 �ǵ�, ���� �� �ڽĿ�����Ʈ ���ֱ�

    }

    // �����Ŵ������� Get���� ������Ʈ ������ �������� �ݵ�� Init ���ֱ�
    public virtual void Init(Vector3 pos, Vector2 dir, int type)
    {
        Debug.Log($"Init�� pos {pos}");
        transform.position = pos;
        Debug.Log($"Init�� transform.position {transform.position}");
        direction = dir.normalized;
        this.type = type;

        // ���⺤�Ϳ� ���缭 �̹��� ȸ��
        RotateToDirection();
    }
    public virtual void Move() // dir �������� speed �ӵ��� �̵�
    {
        transform.Translate(MoveSpeed * Time.deltaTime * direction, Space.World);
    }

    public virtual void TakeDamage(int damage) // �������� ���� �� ü�� ���
    {
        HP -= damage;
        Debug.Log($"{this.name} damaged : {damage} HP : {HP}");
        if (HP <= 0)  // ü�� 0�� �Ǹ� �����鼭 ���ͺ� ���� �� �׼� ����
        {
            Debug.Log($"{this.name} destroyed");
            Die();
        }
    }
    protected virtual void OnEnable()  // ������ƮǮ���� ������ �� Ȱ��ȭ(�ʱ�ȭ)
    {
        transform.rotation = Quaternion.identity;
        isReleased = false;
        HP = BaseHP;
        Attack = BaseAttack;
        MoveSpeed = BaseMoveSpeed;
        AttackSpeed = BaseAttackSpeed;
        AttackStart = BaseAttackStart;
        type = 0;
    }

    protected virtual void OnDisable()  // ��κ��� ���ʹ� Shoot()�� InvokeRepeating �� �����̱⶧���� ��Ȱ��ȭ�� ���
    {                                   // ���� �κ�ũ ���� �ʴ� ��쿩�� ���ɻ� ũ�� ������ ���ٰ� �ϴ� �θ�Ŭ�������� �ϰ�����
        CancelInvoke("Shoot");
    }

    /// <summary>
    /// Destroy() ��� ����մϴ�. ������Ʈ Ǯ�� ����
    /// </summary>
    protected void Release()
    {

        if (!isReleased) // �ߺ����� ����
        {
            isReleased = true;
            PoolManager.instance.Return(gameObject);
        }
    }

    /// <summary>
    /// transform �� �ڽ� ������Ʈ���� �����ϰ� �־ �̷� foreach�� �ڽ��� �ѹ��� �� �� ����
    /// �ı� �ִϸ��̼��� ���۵Ǹ鼭 Engine,Shield ���� ���ֱ�</summary>
    private void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false); // �ڽ� ������Ʈ ��Ȱ��ȭ
        }
    }

    protected virtual void OnBecameInvisible() // ī�޶� ������ ������ ������Ʈ Ǯ�� ��ȯ
    {                               // �ٸ� �Լ����� ��ȯ�ص� �ش� �޼��尡 ����Ǳ� ������(��Ȱ��ȭ �Ǹ鼭 ī�޶󿡼� ������°����� �ν��ϴµ���)
                                    // Release()�� ȣ��Ǹ� isReleased�� True�� �ؼ� �ߺ������� ������
                                    // �̰� ���ϴϱ� ���� ������Ʈ�� �ι��� ��ȯ�ؼ� ������Ʈ Ǯ 10ĭ�� �� ���� ó������ ���ƿ���
                                    // ���� ������Ʈ�� �ι��� ȣ��ż� �ѹ��� �������� �ι辿 �������� ������ �߻�
                                    // �������°ͻӸ��� �ƴ϶� �� ������Ʈ�� �ι� �� �θ��� �̰����� ������ Ŀ�� �� �Ű����ҵ�!!
        Release();
    }

    protected void RotateToDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // �̹����� �Ʒ������̴ϱ� 90�� ����

    }
}
