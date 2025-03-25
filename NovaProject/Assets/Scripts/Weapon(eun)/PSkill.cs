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
    public GameObject effect; // ��ų ��� �� ������ ����Ʈ ������

    // Laser ���� ����
    private Transform pos; // �÷��̾��� ��ġ�� ������ ����
    private float laserT = 4.0f; // ������ ���� �ð� (��)
    private float laserTimer = 0f; // ������ ���� �ð��� �����ϴ� Ÿ�̸�

    // Rocket ���� ����
    private Transform target; // ���� Ÿ���� �Ǵ� ������ ��ġ
    private float homingSpeed = 5.0f; // Ÿ���� �ӵ�
    private float homingRange = 10f; // Ÿ���� ����




    void Start()
    {
        // �÷��̾� ��ġ�� ������ ����
        pos = GameObject.FindWithTag("Player").transform;

        if (skillType == SkillType.Laser)
        {
            // ������ ��ų�� Ȱ��ȭ�Ǹ� Ÿ�̸� ����
            laserTimer = laserT;
        }

        // Rocket ��ų�� ��� Ÿ���� ã���� ����
        if (skillType == SkillType.Rocket)
        {
            FindTarget(); // ���� ����� ���͸� Ÿ������ ã��
        }

        // ��ų 3�� �� �ڵ� ����
        Destroy(gameObject, 3f);

    }

    void Update()
    {
        // Laser ��ų�� �÷��̾� ��ġ�� �°� �̵�
        if (skillType == SkillType.Laser)
        {

            if (pos != null)
            {
                transform.position = pos.position + (Vector3.up * 5.5f);
            }


            // ������ ���� �ð��� ������ ����
            laserTimer -= Time.deltaTime; // �� �����Ӹ��� Ÿ�̸� ����
            if (laserTimer <= 0f)
            {
                Destroy(gameObject); // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ������ ����
            }
        }
        else if (skillType == SkillType.Rocket)
        {
            if (target != null)
            {
                // Ÿ���� ���� ���� �̵� (Ÿ���� ����)
                Vector3 direction = target.position - transform.position;
                float step = homingSpeed * Time.deltaTime; // �̵� �ӵ� ���
                transform.position = Vector2.MoveTowards(transform.position, target.position, step); // Ÿ�� ������ �̵�
            }
            else
            {
                // Ÿ���� ������ ���� �̵�
                transform.Translate(Vector2.up * speed * Time.deltaTime);
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
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // ���� �±׷� �˻�
        float closestDistance = Mathf.Infinity; // �ּ� �Ÿ��� ���Ѵ�� �ʱ�ȭ

        // ��� ���͸� ��ȸ�ϸ鼭 ���� ����� ���͸� ã��
        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position); // ���� �Ѿ� ��ġ�� ���� �Ÿ� ���
            if (distance < closestDistance && distance <= homingRange)
            {
                closestDistance = distance; // �ּ� �Ÿ� ����
                target = monster.transform; // Ÿ������ ����
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
        if (collision.gameObject.CompareTag("Monster"))
        {

            IDamageable obj = collision.GetComponent<IDamageable>();
            if (!obj.IsDead)
            {
                obj.TakeDamage(damage);

                // �浹 ����Ʈ ���� 
                if (effect != null)
                {
                    GameObject effectInstance = Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(effectInstance, 1f);  // ����Ʈ�� 1�� �� �ڵ����� �����ǵ��� ����
                }

                // �Ѿ� ����
                Destroy(gameObject);
            }
        }
    }

    private void ApplySkillEffect(GameObject monster)
    {
        // ��ų ������ ȿ�� ����

/*

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

*/

    }

}