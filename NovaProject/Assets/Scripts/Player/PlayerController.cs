using UnityEngine;
using UnityEngine.InputSystem;

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
    /// ���콺 ������ �÷��̾� ������ �ٶ󺸵��� �����ϴ� �÷���.
    /// </summary>
    public bool useMouseDirection = false;

    /// <summary>
    /// �⺻ ���� ���� ������Ʈ (�÷��̾� �̵� �� ����).
    /// </summary>
    public GameObject BaseEngine;

    /// <summary>
    /// ���� �޽� ���� ���� ������Ʈ (������ ��ȭ �� ���� ���� ����).
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
    /// ���� �� �ִϸ����� (��� ����� ����� ������ �����).
    /// </summary>
    public Animator FrontSideShield;

    /// <summary>
    /// ���� ��Ʈ�ѷ� ���� (�÷��̾��� ���� �߻縦 ������).
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
    /// Rigidbody2D ������Ʈ ���� (���� �̵� ó���� ���� �ʿ���).
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
        inputSystem.Player.Click.performed += _ => Attack(); // Ŭ�� �Է� �̺�Ʈ ��� (���콺 Ŭ���� ���� ó��)
    }

    /// <summary>
    /// ��ü�� ��Ȱ��ȭ�� �� ȣ��Ǹ�, ���� �̺�Ʈ�� �����Ѵ�.
    /// </summary>
    private void OnDisable()
    {
        inputSystem.Player.Attack.performed -= _ => Attack(); // ���� �Է� �̺�Ʈ ����
        inputSystem.Player.Click.performed -= _ => Attack(); // Ŭ�� �Է� �̺�Ʈ ����
        inputSystem.Disable(); // �Է� �ý��� ��Ȱ��ȭ
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǹ�, �÷��̾��� ����� �̵��� ó���Ѵ�.
    /// </summary>
    private void Update()
    {
        direction(); // �÷��̾ �ٶ󺸴� ���� ������Ʈ
        move(); // �÷��̾� �̵� ó��
    }

    /// <summary>
    /// ������ �������� ȣ��Ǹ�, Rigidbody2D�� �̿��� ���� �̵��� ó���Ѵ�.
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * moveInput); // Rigidbody2D ��� �̵� ó��
    }

    /// <summary>
    /// �÷��̾��� ������ ���ϴ� �޼���.
    /// ���콺�� ��ġ�� ���� �ٶ󺸴� ������ �޶�����.
    /// </summary>
    private void direction()
    {
        if (useMouseDirection)
        {
            RotateTowardsMouse(transform); // ���콺�� �ٶ󺸵��� ȸ��
        }
    }

    /// <summary>
    /// �÷��̾��� �̵��� ó���ϴ� �޼���.
    /// �Է°��� �޾� �̵� ������ �����ϰ�, ���� ȿ���� Ȱ��ȭ�Ѵ�.
    /// </summary>
    private void move()
    {
        moveInput = inputSystem.Player.Move.ReadValue<Vector2>(); // �̵� �Է°� ������Ʈ

        if (moveInput.y > 0)
        {
            amBaseEngineEffects.SetBool("power", true); // ���� ȿ�� Ȱ��ȭ
        }
        else
        {
            amBaseEngineEffects.SetBool("power", false); // ���� ȿ�� ��Ȱ��ȭ
        }
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
    /// Ư�� ��ġ���� ���콺 ���� ���͸� ��ȯ�ϴ� �Լ�.
    /// </summary>
    Vector3 GetMouseVectorFromPosition(Vector3 position)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue(); // ���콺 ȭ�� ��ǥ ��������
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0)); // ���콺�� ���� ��ǥ ��ȯ
        mouseWorldPos.z = 0; // 2D ȯ���̹Ƿ� z ���� 0���� ����

        Vector3 mouseDirection = (mouseWorldPos - position).normalized; // ���� ���� ���
        return mouseDirection;
    }

    /// <summary>
    /// ������Ʈ�� ���콺 �������� ȸ����Ű�� �Լ�.
    /// </summary>
    void RotateTowardsMouse(Transform obj)
    {
        Vector3 direction = GetMouseVectorFromPosition(obj.position); // ���� ��ġ���� ���콺 ���� ���� ��������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // ���� ���͸� ������ ��ȯ (���� ����)
        obj.rotation = Quaternion.Euler(0f, 0f, angle); // ȸ�� ����
    }
}
