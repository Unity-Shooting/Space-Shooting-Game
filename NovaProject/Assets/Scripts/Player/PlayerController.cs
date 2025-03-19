using UnityEngine;

/// <summary>
/// ���� ������ �����ϴ� ������.
/// </summary>
enum WeaponType
{
    AutoCannon,  // �ڵ� ����
    BigSpaceGun  // ���� ������
}

/// <summary>
/// �÷��̾��� �̵� �� ������ �����ϴ� ��Ʈ�ѷ� Ŭ����.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// ������� ���� �±� ���ڿ�.
    /// </summary>
    private const string TAG = "PlayerController";

    /// <summary>
    /// �÷��̾� �̵� �ӵ�.
    /// </summary>
    public float speed = 5f;

    /// <summary>
    /// �⺻ ���� ���� ������Ʈ.
    /// </summary>
    public GameObject BaseEngine;

    /// <summary>
    /// ���� �޽� ���� ���� ������Ʈ.
    /// </summary>
    public GameObject BigPulseEngine;

    /// <summary>
    /// �⺻ ���� ȿ�� �ִϸ�����.
    /// </summary>
    public Animator amBaseEngineEffects;

    /// <summary>
    /// ���� �޽� ���� ȿ�� �ִϸ�����.
    /// </summary>
    public Animator amBigPulseEngineEffects;

    /// <summary>
    /// ���� �� �ִϸ�����.
    /// </summary>
    public Animator FrontSideShield;

    /// <summary>
    /// ���� ��Ʈ�ѷ� ����.
    /// </summary>
    public WeaponController weaponCotroller;

    /// <summary>
    /// �Էµ� �̵� ���� ����.
    /// </summary>
    private Vector2 moveInput;

    /// <summary>
    /// ����� �Է��� �����ϴ� Ŀ���� �Է� �ý��� ��ü.
    /// </summary>
    private MyInputSystemActions inputSystem;

    /// <summary>
    /// Rigidbody2D ������Ʈ ����.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// �ʱ�ȭ �۾��� �����ϴ� Awake �޼���.
    /// Rigidbody2D �� �Է� �ý����� �ʱ�ȭ�Ѵ�.
    /// </summary>
    private void Awake()
    {
        inputSystem = new MyInputSystemActions(); // �Է� �ý��� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
    }

    /// <summary>
    /// ��ü�� Ȱ��ȭ�� �� ȣ��Ǹ�, �Է� �ý����� Ȱ��ȭ�ϰ� ���� �̺�Ʈ�� ���ε��Ѵ�.
    /// </summary>
    private void OnEnable()
    {
        inputSystem.Enable(); // �Է� �ý��� Ȱ��ȭ
        inputSystem.Player.Attack.performed += _ => Attack(); // ���� �Է� �̺�Ʈ ���
    }

    /// <summary>
    /// ��ü�� ��Ȱ��ȭ�� �� ȣ��Ǹ�, ���� �̺�Ʈ�� �����Ѵ�.
    /// </summary>
    private void OnDisable()
    {
        inputSystem.Player.Attack.performed -= _ => Attack(); // ���� �Է� �̺�Ʈ ����
        inputSystem.Disable(); // �Է� �ý��� ��Ȱ��ȭ
    }

    /// <summary>
    /// �÷��̾��� ������ �����ϴ� �޼���.
    /// </summary>
    private void Attack()
    {
        Debug.Log($"{TAG} : Attack"); // ���� �߻� �α� ���
        weaponCotroller.Shooting(); // ���� �߻�
    }

    /// <summary>
    /// Unity�� �⺻ Start �޼��� (����� ������ ����).
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǹ�, �÷��̾��� �Է��� �����Ѵ�.
    /// </summary>
    private void Update()
    {
        move();
    }

    /// <summary>
    /// �÷��̾��� �̵��� ó���ϴ� �޼���.
    /// �Է¿� ���� �̵� ������ �����ϰ� ���� ȿ���� �����Ѵ�.
    /// </summary>
    private void move()
    {
        moveInput = inputSystem.Player.Move.ReadValue<Vector2>(); // �̵� �Է°� ������Ʈ
        //Debug.Log(moveInput.y);

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            amBaseEngineEffects.SetBool("power", true);
        }
        else
        {
            amBaseEngineEffects.SetBool("power", false);
        }
    }

    /// <summary>
    /// ������ �������� ȣ��Ǹ�, Rigidbody2D�� �̿��� �̵��� ó���Ѵ�.
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * moveInput); // �̵� ó��
    }
}
