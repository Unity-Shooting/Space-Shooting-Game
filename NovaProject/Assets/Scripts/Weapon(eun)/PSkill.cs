using UnityEngine;

public class PSkill : MonoBehaviour
{
    public float speed = 5.0f; // 스킬 속도
    public int damage = 20; // 스킬 공격력
    public GameObject effect; // 스킬 사용 시 효과 프리팹 (필요한 경우)

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
            // 몬스터에게 피해

            // 스킬 삭제
            Destroy(gameObject);
        }
    }
}
