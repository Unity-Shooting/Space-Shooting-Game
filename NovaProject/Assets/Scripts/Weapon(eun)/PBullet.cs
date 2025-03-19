using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f; // �Ѿ� �ӵ�
    public int attack = 10; // ���ݷ�
    public GameObject effect; // �浹 �� ����Ʈ ������

    void Start()
    {
        
    }

    void Update()
    {
        // ���� �������� �̵�
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        // ȭ�� ������ ������ ����
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // ���Ϳ��� ����

            // �Ѿ� ����
            Destroy(gameObject);
        }
    }
}
