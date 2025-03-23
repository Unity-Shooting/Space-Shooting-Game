using UnityEngine;

public enum SkillType
{
    Laser,
    Rocket,
    Missile
}

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // ��ų �ӵ�
    public int damage = 20; // ��ų �⺻ ���ݷ�
    public SkillType skillType; // ��ų ����
    public GameObject effect; // ��ų ��� �� ȿ�� ������

    // Laser ���� ����
    private Transform pos; // �÷��̾��� ��ġ�� ������ ����
    private float laserT = 4.0f; // ������ ���� �ð� (��)
    private float laserTimer = 0f; // Ÿ�̸� ����

    // Rocket ���� ����
    private Transform target; // Ÿ�� ������ ��ġ
    private float homingSpeed = 5.0f; // Ÿ���� �ӵ�
    private float homingRange = 10f; // Ÿ���� ����

    


    void Start()
    {
        // �÷��̾��� Transform�� �����ͼ� playerTransform�� �Ҵ�
        pos = GameObject.FindWithTag("Player").transform;

        if (skillType == SkillType.Laser)
        {
            // �������� ��ȯ�Ǹ� Ÿ�̸� ����
            laserTimer = laserT;
        }

        // Rocket ��ų�� ��� Ÿ���� ã���� ����
        if (skillType == SkillType.Rocket)
        {
            FindTarget(); // ���� ����� ���͸� Ÿ������ ã��
        }
    }

    void Update()
    {
        // Laser ��ų�� �÷��̾� ��ġ�� �°� �̵�
        if (skillType == SkillType.Laser)
        {
            if (pos != null)
            {
                // �������� �÷��̾� ��ġ�� ���� �����Ǹ�, �÷��̾� ��ġ���� y�� 5.5��ŭ ���� ��ġ
                transform.position = pos.position + (Vector3.up * 5.5f);
            }

            // �������� Ȱ��ȭ�� �ð��� ������ ����
            laserTimer -= Time.deltaTime; // �� �����Ӹ��� Ÿ�̸� ����

            if (laserTimer <= 0f)
            {
                Destroy(gameObject); // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ������ ����
            }
        }
        if (skillType == SkillType.Rocket)
        {
            if (target != null)
            {
                // Ÿ���� ���� ���� �̵� (Ÿ���� ����)
                Vector3 direction = target.position - transform.position;
                float step = homingSpeed * Time.deltaTime; // �̵� �ӵ� ���
                transform.position = Vector2.MoveTowards(transform.position, target.position, step); // Ÿ�� ������ �̵�
            }
        }
        else
        {
            // �ٸ� ��ų�� ��� �⺻������ �������� �̵�
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    

    // Ÿ���� ���� ������ ���� ����� ���͸� ã�� �޼ҵ�
    private void FindTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float closestDistance = Mathf.Infinity; // ���� ����� ������ �Ÿ�

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance && distance <= homingRange)
            {
                closestDistance = distance;
                target = monster.transform; // ���� ����� ���͸� Ÿ������ ����
            }
        }
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


            // �浹 ����Ʈ ���� (�ʿ��� ���)
            if (effect != null)
            {
                GameObject effectInstance = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(effectInstance, 1f);  // ����Ʈ�� 1�� �� �ڵ����� �����ǵ��� ����
            }

            // �浹 �� ��ų ����
            if (skillType == SkillType.Rocket)
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
            case SkillType.Laser:
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            case SkillType.Rocket:
                // ȭ�� ȿ��
                monster.GetComponent<monster>().TakeDamage(damage);
                monster.GetComponent<monster>().ApplyStatusEffect("Burn", 5); // 5�� ȭ��
                break;

            case SkillType.Missile:
                // ���� ȿ��
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            default:
                Debug.LogError("�� �� ���� ��ų ����");
                break;
        }
    }

    /*

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            IDamageable obj = collision.GetComponent<IDamageable>();
            obj.TakeDamage(damage);
        }
    }

*/

}
