using UnityEngine;

public enum SkillType
{
    Laser,
    Rocket,
    Missile
}

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // 스킬 속도
    public int damage = 20; // 스킬 기본 공격력
    public SkillType skillType; // 스킬 유형
    public GameObject effect; // 스킬 사용 시 효과 프리팹

    // Laser 관련 변수
    private Transform pos; // 플레이어의 위치를 저장할 변수
    private float laserT = 4.0f; // 레이저 지속 시간 (초)
    private float laserTimer = 0f; // 타이머 변수

    // Rocket 관련 변수
    private Transform target; // 타겟 몬스터의 위치
    private float homingSpeed = 5.0f; // 타겟팅 속도
    private float homingRange = 10f; // 타겟팅 범위

    


    void Start()
    {
        // 플레이어의 Transform을 가져와서 playerTransform에 할당
        pos = GameObject.FindWithTag("Player").transform;

        if (skillType == SkillType.Laser)
        {
            // 레이저가 소환되면 타이머 시작
            laserTimer = laserT;
        }

        // Rocket 스킬인 경우 타겟을 찾도록 설정
        if (skillType == SkillType.Rocket)
        {
            FindTarget(); // 가장 가까운 몬스터를 타겟으로 찾음
        }
    }

    void Update()
    {
        // Laser 스킬만 플레이어 위치에 맞게 이동
        if (skillType == SkillType.Laser)
        {
            if (pos != null)
            {
                // 레이저는 플레이어 위치에 따라 고정되며, 플레이어 위치에서 y축 5.5만큼 위에 위치
                transform.position = pos.position + (Vector3.up * 5.5f);
            }

            // 레이저가 활성화된 시간이 지나면 삭제
            laserTimer -= Time.deltaTime; // 매 프레임마다 타이머 감소

            if (laserTimer <= 0f)
            {
                Destroy(gameObject); // 타이머가 0 이하가 되면 레이저 삭제
            }
        }
        if (skillType == SkillType.Rocket)
        {
            if (target != null)
            {
                // 타겟을 향해 로켓 이동 (타겟을 추적)
                Vector3 direction = target.position - transform.position;
                float step = homingSpeed * Time.deltaTime; // 이동 속도 계산
                transform.position = Vector2.MoveTowards(transform.position, target.position, step); // 타겟 쪽으로 이동
            }
        }
        else
        {
            // 다른 스킬일 경우 기본적으로 위쪽으로 이동
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    

    // 타겟팅 범위 내에서 가장 가까운 몬스터를 찾는 메소드
    private void FindTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float closestDistance = Mathf.Infinity; // 가장 가까운 몬스터의 거리

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance && distance <= homingRange)
            {
                closestDistance = distance;
                target = monster.transform; // 가장 가까운 몬스터를 타겟으로 설정
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
        // 스킬이 몬스터와 충돌했을 때
        if (collision.CompareTag("Monster"))
        {
            // 몬스터와 충돌 시 스킬별 효과
            ApplySkillEffect(collision.gameObject);


            // 충돌 이펙트 생성 (필요한 경우)
            if (effect != null)
            {
                GameObject effectInstance = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(effectInstance, 1f);  // 이펙트가 1초 후 자동으로 삭제되도록 설정
            }

            // 충돌 후 스킬 삭제
            if (skillType == SkillType.Rocket)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ApplySkillEffect(GameObject monster)
    {
        // 스킬 유형별 효과 적용
        switch (skillType)
        {
            case SkillType.Laser:
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            case SkillType.Rocket:
                // 화상 효과
                monster.GetComponent<monster>().TakeDamage(damage);
                monster.GetComponent<monster>().ApplyStatusEffect("Burn", 5); // 5초 화상
                break;

            case SkillType.Missile:
                // 관통 효과
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            default:
                Debug.LogError("알 수 없는 스킬 유형");
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
