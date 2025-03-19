using UnityEngine;

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // ��ų �ӵ�
    public int damage = 20; // ��ų ���ݷ�
    public GameObject effect; // ��ų ��� �� ȿ�� ������ (�ʿ��� ���)

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
        // ȭ�� ������ ������ ��ų ����
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ų�� ���Ϳ� �浹���� ��
        if (collision.CompareTag("Monster"))
        {
            // ���Ϳ��� ����

            // ��ų ����
            Destroy(gameObject);
        }
    }
}
