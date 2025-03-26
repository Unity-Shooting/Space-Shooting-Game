using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 플레이어의 체력을 관리하는 클래스입니다.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    private const string TAG = "Health"; // 로그 출력 시 사용될 태그입니다.

    /// <summary>
    /// 플레이어의 스프라이트를 저장하는 변수입니다.
    /// </summary>
    public Sprite[] baseSprites;

    /// <summary>
    /// 플레이어의 현재 체력을 저장하는 변수입니다.
    /// </summary>
    public int hp;

    /// <summary>
    /// 대미지를 받은 후 다시 대미지를 받을 수 있도록 회복되는 시간(초 단위)
    /// </summary>
    public float invincibleTime = 1f;

    /// <summary>
    /// 현재 대미지를 받을 수 있는지 여부를 나타내는 플래그
    /// </summary>
    public bool canTakeDamage = true;

    /// <summary>
    /// 실드 상태에 따라 대미지 여부
    /// </summary>
    public bool isShieldOn = false;

    /// <summary>
    /// 싱글톤 인스턴스가 초기화될 때 호출됩니다.
    /// <para>Awake()에서 GameManager의 로그 상태를 체크하고 로그를 출력할 수 있습니다.</para>
    /// </summary>
    protected override void Awake()
    {
        // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake before base.Awake");
        base.Awake(); // 싱글톤 패턴에서 기본 Awake 실행
        // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake after base.Awake");
    }

    /// <summary>
    /// 플레이어가 대미지를 받을 때 호출되는 메서드입니다.
    /// <para>체력이 감소하고, 화면 효과 및 무적 시간 처리를 합니다.</para>
    /// </summary>
    /// <param name="num">입력된 대미지 값</param>
    public void TakeDamage(int num)
    {
        if (isShieldOn) return;
        if (!canTakeDamage) return; // 무적 상태라면 대미지를 받지 않음
        canTakeDamage = false; // 대미지를 받았으므로 무적 상태로 변경

        // hp에 따라 sprite 변경
        if (--hp < 1) hp = 0; // 체력 감소 후 0 이하로 내려가지 않도록 제한
        SpriteRenderer pSR = PlayerController.Instance.Base.GetComponent<SpriteRenderer>();
        if (hp == 0)
        {
            pSR.color = Color.red;
            GameManager.Instance.GameOver();
        }
        else if (hp < 3) pSR.sprite = baseSprites[3];
        else if (hp < 6) pSR.sprite = baseSprites[2];
        else if (hp < 9) pSR.sprite = baseSprites[1];
        else pSR.sprite = baseSprites[0];

        // if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] hp : {hp}"); // 체력 감소 후 로그 출력
        PlayerController.Instance.Base.gameObject.GetComponent<Flash>().Run(); // 플레이어가 피격 시 깜빡이는 효과 실행
        StartCoroutine(RecoveryRoutine()); // 일정 시간 후 다시 대미지를 받을 수 있도록 무적 해제
        ScreenShakeManager.Instance.ShakeScreen(); // 화면 흔들림 효과 적용
        SFXManager.Instance.PlayDamageSound();
    }

    /// <summary>
    /// 일정 시간이 지나면 다시 대미지를 받을 수 있도록 설정하는 코루틴
    /// </summary>
    IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(invincibleTime); // 회복 시간만큼 대기
        canTakeDamage = true; // 다시 대미지를 받을 수 있도록 설정
    }

    /// <summary>
    /// 충돌 감지 시 실행되는 함수입니다.
    /// </summary>
    /// <param name="collision">충돌한 객체의 Collider2D 정보</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] OnTriggerEnter2D. tag : {collision.gameObject.tag}");
        if (collision.gameObject.tag == "Bullet") // 충돌한 객체가 총알(Bullet)일 경우 대미지를 받음
        {
            // Destroy(collision.gameObject); // 총알 객체 제거 (주석 처리됨)
            TakeDamage(1); // 체력 1 감소
        }
    }
}
