using UnityEngine;

/// <summary>
/// 무기 컨트롤러 인터페이스.
/// 모든 무기는 이 인터페이스를 구현하여 공격 메서드를 제공해야 한다.
/// </summary>
public interface IWeaponController
{
    /// <summary>
    /// 무기를 발사하는 메서드.
    /// </summary>
    void Shooting();
}
