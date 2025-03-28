using TMPro;
using UnityEngine;

/// <summary>
/// 탄환(Bullet)의 이동 및 속성을 제어하는 클래스. 
/// </summary>
public class TextBullet1 : MonoBehaviour
{
    private string TAG = "TextBullet1";

    /// <summary>
    /// 탄환의 이동 속도.
    /// </summary>
    public float speed = 7f;

    /// <summary>
    /// 탄환이 적중 시 입히는 피해량.
    /// </summary>
    public float damage = 5f;

    private bool isShoot = false;

    /// <summary>
    /// Rigidbody2D 컴포넌트 참조.
    /// </summary>
    private Rigidbody2D rb;

    private Vector3 direction;

    public string[] strs = {
        "10분 쉬었다할게요", "체크해주세요~", "점심 맛있게 드세요~", "벌써 50분이야?",
        "오늘도 두 분 모시겠습니다.", "모바일게임 숙제하면서 하시먼 안돼요",
        "오늘도 수고 많으셨습니다.", "인원 체크", "텍스트알피지하고와야겠네",
        "쉬었다 하겠습니다", "갓겜인데?"
    };

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        GetComponent<TextMeshPro>().text = strs[Random.Range(0, strs.Length)];
    }

    void Start()
    {
        SetDirection();
    }

    /// <summary>
    /// 탄환의 방향을 플레이어를 향하도록 설정합니다.
    /// 플레이어의 위치를 기준으로 회전 각도를 계산하여 적용합니다.
    /// </summary>
    void SetDirection()
    {
        Vector3 dir = PlayerController.Instance.transform.position - transform.position;
        float angle = Vector2.SignedAngle(-transform.right, dir);
        // Debug.Log($"{TAG} angle: {angle}");
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);
        // Debug.Log($"{TAG} angle: {transform.forward}");
    }

    void Update()
    {
        if(!isShoot) SetDirection();
        else transform.Translate(speed * Time.deltaTime * Vector3.left);
    }

    public void Shooting() // 애니메이션에서 호출
    {
        isShoot = true;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Debug.Log($"{TAG} OnTriggerEnter2D Player");
            PlayerHealth.Instance.TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
