using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어의 체력을 관리하는 클래스입니다.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    private const string TAG = "Health";

    /// <summary>
    /// 플레이어의 체력을 저장하는 변수입니다.
    /// </summary>
    public int hp;

    public float recoveryTime = 1f;

    private bool canTakeDamage = true;

    /// <summary>
    /// 싱글톤 인스턴스가 초기화될 때 호출됩니다.
    /// <para>Awake()에서 GameManager의 로그 상태를 체크하고 로그를 출력할 수 있습니다.</para>
    /// </summary>
    protected override void Awake()
    {
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake before base.Awake");
        base.Awake();
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake after base.Awake");
    }

    /// <summary>
    /// 플레이어가 데미지를 받을 때 호출되는 메서드입니다.
    /// <para>체력이 감소하고, 로그로 체력 상태를 출력합니다.</para>
    /// </summary>
    /// <param name="num">입력된 데미지 수치</param>
    public void TakeDamage(int num)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        if (--hp < 1) hp = 0; // 체력 감소 후 최소값 0으로 제한
        //if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] hp : {hp}"); // 체력 감소 후 로그 출력
        Flash flash = PlayerController.Instance.Base.gameObject.GetComponent<Flash>();
        flash.Run();
        StartCoroutine(RecoveryRoutine());
        //PlayerController.Instance.FrontSideShield.gameObject.SetActive(true);
    }

    IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(recoveryTime);
        canTakeDamage = true;
        //PlayerController.Instance.FrontSideShield.gameObject.SetActive(false);
    }

    /// <summary>
    /// 충돌 감지 시 실행되는 함수.
    /// </summary>
    /// <param name="collision">충돌한 객체의 Collider2D</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: 화면 흔들림 효과 적용

        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] OnTriggerEnter2D. tag : {collision.gameObject.tag}");

        if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(collision.gameObject);
            TakeDamage(1); // 체력 감소
        }
    }
}
