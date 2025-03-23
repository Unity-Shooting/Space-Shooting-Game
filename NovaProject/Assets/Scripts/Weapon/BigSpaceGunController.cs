using UnityEngine;

/// <summary>
/// 대형 우주포(BigSpaceGun)를 제어하는 컨트롤러 클래스.
/// IWeaponController 인터페이스를 구현한다.
/// </summary>
public class BigSpaceGunController : MonoBehaviour, IWeaponController
{
    /// <summary>
    /// 발사될 탄환 프리팹.
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// 발사 위치.
    /// </summary>
    public Transform launcher1;

    /// <summary>
    /// Unity의 기본 Start 메서드 (현재는 사용되지 않음).
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// 매 프레임마다 호출되는 Unity의 기본 Update 메서드 (현재는 사용되지 않음).
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// 대형 우주포를 발사하는 메서드.
    /// </summary>
    public void Shooting()
    {
        // TODO: 애니메이션 재생 로직 추가
        Instantiate(bullet, launcher1.position, PlayerController.Instance.transform.rotation); // 발사 위치에서 탄환 생성
    }
}
