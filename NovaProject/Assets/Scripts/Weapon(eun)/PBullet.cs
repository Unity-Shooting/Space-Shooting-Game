using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f; // 총알 속도
    public int damage = 10; // 총알 공격력
    public GameObject effect; // 충돌 시 생성될 이펙트 프리팹

    public bool isHoming = false; // 총알이 타겟을 추적하는지 여부 (E_Bullet에서 사용)
    public float homingSpeed = 4f; // 타겟팅 속도
    public float homingRange = 10f; // 타겟팅 범위

    private Transform target; // 타겟 몬스터의 위치

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isHoming)
        {
            // 타겟 찾기 (E_Bullet일 경우)
            FindTarget();
        }

        // 총알 2초 후 자동 삭제
        Destroy(gameObject, 2f);

    }

    void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy) 
        {
            // 타겟을 향해 이동 (타겟을 추적)
            Vector3 direction = target.position - transform.position; // 타겟 방향 계산
            float step = homingSpeed * Time.deltaTime; // 이동 속도 계산
            transform.position = Vector2.MoveTowards(transform.position, target.position, step); // 타겟 방향으로 이동


            // 회전 (타겟을 향해 회전) 


        }
        else
        {
            // 타겟이 없을 경우 위로만 이동
            rb.linearVelocity = Vector2.up * speed; // Rigidbody2D로 위쪽으로 이동
        }
    }

    private void FindTarget()
    {
        // 타겟팅 범위 내에서 가장 가까운 몬스터 찾기
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // 몬스터 태그로 검색
        float closeDistance = homingRange;


        // 모든 몬스터를 순회하면서 가장 가까운 몬스터를 찾음
        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position); // 현재 총알 위치와 몬스터 거리 계산

            if (distance < closeDistance && distance <= homingRange)
            {
                closeDistance = distance; // 최소 거리 갱신
                target = monster.transform; // 타겟으로 설정
            }
        }
    }


    private void OnBecameInvisible()
    {
        // 화면 밖으로 나가면 삭제
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

                // 충돌 이펙트 생성 
                if (effect != null)
                {
                    GameObject effectInstance = Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(effectInstance, 1f);  // 이펙트가 1초 후 자동으로 삭제되도록 설정
                }

                // 총알 삭제
                Destroy(gameObject);
            }
        }
    }
}
