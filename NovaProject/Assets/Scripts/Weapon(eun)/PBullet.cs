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
            // ���� ��ũ��Ʈ ��������
            monster monster = collision.GetComponent<monster>();
            if (monster != null)
            {
                monster.TakeDamage(attack); // ���Ϳ��� ������ ����
            }

            // �浹 ����Ʈ ���� (�ʿ��� ���)
            if (effect != null)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
            }

            // �Ѿ� ����
            Destroy(gameObject);
        }
    }
}
