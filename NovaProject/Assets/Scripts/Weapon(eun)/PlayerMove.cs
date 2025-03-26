using UnityEngine;


// �׽�Ʈ�� ���� �ӽ÷� PlayerMove ��ũ��Ʈ �ۼ��߽��ϴ�!

public class PlayerMove : MonoBehaviour
{
    // WeaponManager ����
    public WeaponEun.WeaponManager weaponManager;

    // �̵� �ӵ�
    public float speed = 5f;

    // Rigidbody2D ����
    private Rigidbody2D rb;

    // �̵� �Է°�
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D �ʱ�ȭ
    }



    void Start()
    {
        
    }

    void Update()
    {

        // �̵� �Է� ������Ʈ
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        

    }

    private void FixedUpdate()
    {
        // Rigidbody2D�� �̿��� �̵� ó��
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
