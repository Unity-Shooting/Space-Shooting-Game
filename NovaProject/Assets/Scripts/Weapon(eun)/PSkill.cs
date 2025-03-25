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
    public GameObject effect; // 스킬 사용 시 생성될 이펙트 프리팹

    // Laser 관련 변수
    private Transform pos; // 플레이어의 위치를 저장할 변수
    private float laserT = 4.0f; // 레이저 지속 시간 (초)
    private float laserTimer = 0f; // 레이저 지속 시간을 추적하는 타이머

    // Rocket 관련 변수
    private Transform target; // 로켓 타겟이 되는 몬스터의 위치
    private float homingSpeed = 5.0f; // 타겟팅 속도
    private float homingRange = 10f; // 타겟팅 범위




    void Start()
    {
        // 플레이어 위치를 가져와 저장
        pos = GameObject.FindWithTag("Player").transform;

        if (skillType == SkillType.Laser)
        {
            // 레이저 스킬이 활성화되면 타이머 시작
            laserTimer = laserT;
        }

        // Rocket 스킬인 경우 타겟을 찾도록 설정
        if (skillType == SkillType.Rocket)
        {
            FindTarget(); // 가장 가까운 몬스터를 타겟으로 찾음
        }

        // 스킬 3초 후 자동 삭제
        Destroy(gameObject, 3f);

    }

    void Update()
    {
        // Laser 스킬만 플레이어 위치에 맞게 이동
        if (skillType == SkillType.Laser)
        {

            if (pos != null)
            {
                transform.position = pos.position + (Vector3.up * 5.5f);
            }


            // 레이저 지속 시간이 끝나면 삭제
            laserTimer -= Time.deltaTime; // 매 프레임마다 타이머 감소
            if (laserTimer <= 0f)
            {
                Destroy(gameObject); // 타이머가 0 이하가 되면 레이저 삭제
            }
        }
        else if (skillType == SkillType.Rocket)
        {
            if (target != null)
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
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // 몬스터 태그로 검색
        float closestDistance = Mathf.Infinity; // 최소 거리를 무한대로 초기화

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

                // 총알 삭제
                Destroy(gameObject);
            }
        }
    }

    private void ApplySkillEffect(GameObject monster)
    {
        // 스킬 유형별 효과 적용

/*

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

*/

    }

}