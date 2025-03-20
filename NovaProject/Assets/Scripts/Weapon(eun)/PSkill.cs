using UnityEngine;

public enum SkillType
{
    Spark,  // 전기 느낌, 타겟팅, 빠른 공속, 약한 데미지
    Bolt,   // 불 느낌, 직접 조준, 강한 데미지, 화상 효과
    Pulse   // 물 느낌, 적 관통, 느린 속도, 중간 데미지
}

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // 스킬 속도
    public int damage = 20; // 스킬 기본 공격력
    public SkillType skillType; // 스킬 유형
    public GameObject effect; // 스킬 사용 시 효과 프리팹

    void Start()
    {

    }

    void Update()
    {
        // 위쪽 방향으로 이동
        transform.Translate(Vector2.up * speed * Time.deltaTime);
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

            // 충돌 후 스킬 삭제 (Spark는 삭제하지 않을 수도 있음)
            if (skillType != SkillType.Pulse) // Pulse는 관통 효과를 위해 삭제 안 함
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
            case SkillType.Spark:
                // 빠른 공속과 약한 데미지
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            case SkillType.Bolt:
                // 강한 데미지와 화상 효과
                monster.GetComponent<monster>().TakeDamage(damage);
                monster.GetComponent<monster>().ApplyStatusEffect("Burn", 5); // 5초 화상
                break;

            case SkillType.Pulse:
                // 중간 데미지와 관통 효과
                monster.GetComponent<monster>().TakeDamage(damage);
                break;

            default:
                Debug.LogError("알 수 없는 스킬 유형");
                break;
        }
    }
}
