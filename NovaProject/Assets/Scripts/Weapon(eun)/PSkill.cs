using UnityEngine;

public enum SkillType
{
    Missile,
    Laser,
    Bomb
}

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // 스킬 속도
    public int damage = 20; // 스킬 기본 공격력
    public SkillType skillType; // 스킬 유형
    public GameObject effect; // 스킬 사용 시 생성될 이펙트 프리팹

    // Missile 관련 변수
    private Transform target; // 로켓 타겟이 되는 몬스터의 위치
    private float homingSpeed = 5.0f; // 타겟팅 속도
    private float homingRange = 10f; // 타겟팅 범위

    public Transform firePoint1, firePoint2;

    void Start()
    {
        // Missile 스킬인 경우 타겟을 찾도록 설정
        if (skillType == SkillType.Missile)
        {
            FindTarget(); // 가장 가까운 몬스터를 타겟으로 찾음
        }

        // 레이저는 삭제 타이머를 설정하지 않음
        if (skillType != SkillType.Laser)
        {
            Destroy(gameObject, 3f); // 4초 후 자동 삭제
        }
    }

    void Update()
    {
        if (skillType == SkillType.Missile)
        {
            if (target != null && target.gameObject.activeInHierarchy)
            {
                // 타겟을 향해 로켓 이동 (타겟을 추적)
                Vector3 direction = target.position - transform.position;
                float step = homingSpeed * Time.deltaTime; // 이동 속도 계산
                transform.position = Vector2.MoveTowards(transform.position, target.position, step); // 타겟 쪽으로 이동
            }
            else
            {
                // 타겟이 없으면 위로 이동
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            Debug.Log("스킬타입 : 미사일");
        }
        else if (skillType == SkillType.Bomb)
        {
            // 다른 스킬일 경우 기본적으로 위쪽으로 이동
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (skillType == SkillType.Laser)
        {
            Debug.Log("스킬타입 : 레이저");

        }
    }

    // 타겟팅 범위 내에서 가장 가까운 몬스터를 찾는 메소드
    private void FindTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // 몬스터 태그로 검색
        float closestDistance = homingRange;

        // 모든 몬스터를 순회하면서 가장 가까운 몬스터를 찾음
        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position); // 현재 총알 위치와 몬스터 거리 계산
            if (distance < closestDistance && distance <= homingRange)
            {
                closestDistance = distance; // 최소 거리 갱신
                target = monster.transform; // 타겟으로 설정
            }
        }
    }

    private void OnBecameInvisible()
    {
        // 화면 밖으로 나가면 스킬 삭제
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

                if(skillType != SkillType.Laser)
                {
                    // 총알 삭제
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
