using UnityEngine;

public enum SkillType
{
    Spark,  // ���� ����, Ÿ����, ���� ����, ���� ������
    Bolt,   // �� ����, ���� ����, ���� ������, ȭ�� ȿ��
    Pulse   // �� ����, �� ����, ���� �ӵ�, �߰� ������
}

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // ��ų �ӵ�
    public int damage = 20; // ��ų �⺻ ���ݷ�
    public SkillType skillType; // ��ų ����
    public GameObject effect; // ��ų ��� �� ȿ�� ������

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
            // ���Ϳ� �浹 �� ��ų�� ȿ��
            ApplySkillEffect(collision.gameObject);

            // �浹 �� ��ų ���� (Spark�� �������� ���� ���� ����)
            if (skillType != SkillType.Pulse) // Pulse�� ���� ȿ���� ���� ���� �� ��
            {
                Destroy(gameObject);
            }
        }
    }

    private void ApplySkillEffect(GameObject monster)
    {
        // ��ų ������ ȿ�� ����
        switch (skillType)
        {
            case SkillType.Spark:
                // ���� ���Ӱ� ���� ������
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            case SkillType.Bolt:
                // ���� �������� ȭ�� ȿ��
                monster.GetComponent<monster>().TakeDamage(damage);
                monster.GetComponent<monster>().ApplyStatusEffect("Burn", 5); // 5�� ȭ��
                break;

            case SkillType.Pulse:
                // �߰� �������� ���� ȿ��
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            default:
                Debug.LogError("�� �� ���� ��ų ����");
                break;
        }
    }
}
