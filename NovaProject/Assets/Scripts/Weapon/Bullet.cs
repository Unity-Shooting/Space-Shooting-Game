using UnityEngine;

/// <summary>
/// źȯ(Bullet)�� �̵� �� �Ӽ��� �����ϴ� Ŭ����.
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// źȯ�� �̵� �ӵ�.
    /// </summary>
    public float speed = 3f;

    /// <summary>
    /// źȯ�� ���� �� ������ ���ط�.
    /// </summary>
    public float damage = 3f;

    /// <summary>
    /// Rigidbody2D ������Ʈ ����.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// ��ü�� ������ �� ȣ��Ǹ�, Rigidbody2D�� �ʱ�ȭ�Ѵ�.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǹ�, źȯ�� ������Ų��.
    /// </summary>
    void Update()
    {
        // rb.MovePosition(rb.position + speed * Time.deltaTime * Vector2.up); // �ʹ� ������ ������� ����
        transform.Translate(speed * Time.deltaTime * Vector2.up); // źȯ �̵�
    }
}
