using UnityEngine;

/// <summary>
/// 자동 대포(AutoCannon)를 제어하는 컨트롤러 클래스.
/// IWeaponController 인터페이스를 구현한다.
/// </summary>
public class AutoCannonController : MonoBehaviour, IWeaponController
{
    /// <summary>
    /// 발사될 탄환 프리팹.
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// 오토캐논 애니메이터.
    /// </summary>
    private Animator am;

    /// <summary>
    /// 첫 번째 발사 위치.
    /// </summary>
    public Transform launcher1;

    /// <summary>
    /// 두 번째 발사 위치.
    /// </summary>
    public Transform launcher2;

    /// <summary>
    /// Unity의 기본 Awake 메서드 
    /// </summary>
    void Awake()
    {
        am = GetComponent<Animator>();
    }

    /// <summary>
    /// 매 프레임마다 호출되는 Unity의 기본 Update 메서드 (현재는 사용되지 않음).
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// 자동 대포를 발사하는 메서드.
    /// </summary>
    public void Shooting()
    {
        am.SetTrigger("shoot");
        SFXManager.Instance.ShootSound();
        Instantiate(bullet, launcher1.position, PlayerController.Instance.transform.rotation); // 첫 번째 발사 위치에서 탄환 생성
        Instantiate(bullet, launcher2.position, PlayerController.Instance.transform.rotation); // 두 번째 발사 위치에서 탄환 생성
    }
}
