using UnityEngine;

/// <summary>
/// 플레이어의 무기를 관리하는 컨트롤러 클래스.
/// </summary>
public class WeaponController : MonoBehaviour
{
    /// <summary>
    /// 자동 대포(AutoCannon) 컨트롤러.
    /// </summary>
    public AutoCannonController autoCannonController;

    /// <summary>
    /// 대형 우주포(BigSpaceGun) 컨트롤러.
    /// </summary>
    public BigSpaceGunController bigSpaceGunController;

    /// <summary>
    /// Unity의 기본 Start 메서드 (현재는 사용되지 않음).
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// 매 프레임마다 호출되며, 무기 변경 기능을 처리할 예정.
    /// </summary>
    void Update()
    {
        // TODO: 무기 변경 로직 추가
    }

    /// <summary>
    /// 현재 선택된 무기를 발사하는 메서드.
    /// </summary>
    public void Shooting()
    {
        autoCannonController.Shooting(); // 자동 대포 발사
        //bigSpaceGunController.Shooting(); // 대형 우주포 발사
    }
}
