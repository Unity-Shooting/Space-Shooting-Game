using UnityEngine;

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
    /// �÷��̾��� ������ �����ϴ� �޼���.
    /// </summary>
    private void Attack()
    {
        Debug.Log($"{TAG} : Attack"); // ���� �߻� �α� ���
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
        moveInput = inputSystem.Player.Move.ReadValue<Vector2>(); // �̵� �Է°� ������Ʈ
    }

    /// <summary>
    /// ������ �������� ȣ��Ǹ�, Rigidbody2D�� �̿��� �̵��� ó���Ѵ�.
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position +  speed * Time.deltaTime * moveInput); // �̵� ó��
    }
}
