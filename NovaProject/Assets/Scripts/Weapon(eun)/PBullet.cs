using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f; // 총알 속도
    public int attack = 10; // 공격력
    public GameObject effect; // 충돌 시 이펙트 프리팹

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
        // 화면 밖으로 나가면 삭제
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // 몬스터에게 피해

            // 총알 삭제
            Destroy(gameObject);
        }
    }
}
