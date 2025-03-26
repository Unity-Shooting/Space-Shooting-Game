using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f; // �Ѿ� �ӵ�
    public int damage = 10; // ���ݷ�
    public GameObject effect; // �浹 �� ����Ʈ ������

    public bool isHoming = false; // Ÿ���� ����(E_Bullet�� Ȱ��ȭ)
    public float homingSpeed = 4f; // Ÿ���� �ӵ�
    public float homingRange = 10f; // Ÿ���� ����

    private Transform target; // Ÿ�� ������ ��ġ

    void Start()
    {
        if (isHoming)
        {
            // Ÿ�� ã�� (E_Bullet�� ���)
            FindTarget();
        }
    }

    void Update()
    {
        if(isHoming && target != null)
        {
            // Ÿ���� ���� �̵� (Ÿ���� ����)
            Vector3 direction = target.position - transform.position;
            float step = homingSpeed * Time.deltaTime; // �̵� �ӵ� ��ꤤ
            transform.position = Vector2.MoveTowards(transform.position, target.position, step); // Ÿ�� ������ �̵�


            // ȸ�� (Ÿ���� ���� ȸ��) 


        }
        else
        {
            // Ÿ���� ������ �⺻���� ���� �������� �̵�
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        // Ÿ���� ���� ������ ���� ����� ���� ã��
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float closeDistance = Mathf.Infinity; // ���� ����� ������ �Ÿ�

        // ��� ���͸� ��ȸ�ϸ鼭 ���� ����� ���͸� ã��
        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if(distance < closeDistance && distance <= homingRange) 
            {
                closeDistance = distance; // �ּ� �Ÿ� ����
                target = monster.transform; // Ÿ������ ����
            }
        }
    }

    private void OnBecameInvisible()
    {
        // ȭ�� ������ ������ ����
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {

            IDamageable obj = collision.GetComponent<IDamageable>();
            obj.TakeDamage(damage);

            // �浹 ����Ʈ ���� (�ʿ��� ���)
            if (effect != null)
            {
                GameObject effectInstance = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(effectInstance, 1f);  // ����Ʈ�� 1�� �� �ڵ����� �����ǵ��� ����
            }

            // �Ѿ� ����
            Destroy(gameObject);
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
