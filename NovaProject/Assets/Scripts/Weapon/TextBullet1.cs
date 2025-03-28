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

    public TMP_Text text;

    private bool isShoot = false;

    private string[] strs = {
        "10분 쉬었다할게요", "체크해주세요~", "점심 맛있게 드세요~", "벌써 50분이야?", 
        "오늘도 두 분 모시겠습니다.", "모바일게임 숙제하면서 하시먼 안돼요", "오늘도 수고 많으셨습니다.", 
        "인원 체크", "텍스트알피지하고와야겠네", "쉬었다 하겠습니다", "갓겜인데?", "이십사수매화검법!!", 
        "슈팅게임 마스터", "카타나제로 마스터", "슈팅게임 마스터시켜드림", "1.슈팅마스터한거같다.", 
        "2.아직멀었다", "1945마무리!", "마지막으로 하시고싶으신 말씀", "마이크 되십니까?",
        "어제 실행됐는데 왜 지금 안되지?"
    };

    void Start()
    {
        int randNum = Random.Range(0, strs.Length);
        // if(GameManager.Instance.logOn) Debug.Log($"{TAG} randNum: {randNum}");
        text.text = strs[randNum];
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
            // PoolManager.instance.Return(gameObject);
        }
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
        // PoolManager.instance.Return(gameObject);
    }

}
