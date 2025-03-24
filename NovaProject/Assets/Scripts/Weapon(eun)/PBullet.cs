using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f; // 총알 속도
    public int damage = 10; // 공격력
    public GameObject effect; // 충돌 시 이펙트 프리팹

    public bool isHoming = false; // 타겟팅 여부(E_Bullet만 활성화)
    public float homingSpeed = 4f; // 타겟팅 속도
    public float homingRange = 10f; // 타겟팅 범위

    private Transform target; // 타겟 몬스터의 위치

    void Start()
    {
        if (isHoming)
        {
            // 타겟 찾기 (E_Bullet일 경우)
            FindTarget();
        }

        // 5초 후 자동 삭제
        Destroy(gameObject, 5f);

    }

    void Update()
    {
        if(isHoming && target != null)
        {
            // 타겟을 향해 이동 (타겟을 추적)
            Vector3 direction = target.position - transform.position;
            float step = homingSpeed * Time.deltaTime; // 이동 속도 계산ㄴ
            transform.position = Vector2.MoveTowards(transform.position, target.position, step); // 타겟 쪽으로 이동


            // 회전 (타겟을 향해 회전) 


        }
        else
        {
            // 타겟이 없으면 기본적인 위쪽 방향으로 이동
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        // 타겟팅 범위 내에서 가장 가까운 몬스터 찾기
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float closeDistance = Mathf.Infinity; // 가장 가까운 몬스터의 거리

        // 모든 몬스터를 순회하면서 가장 가까운 몬스터를 찾음
        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if(distance < closeDistance && distance <= homingRange) 
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

                // 충돌 이펙트 생성 (필요한 경우)
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
