using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// 화면 흔들림 효과를 관리하는 클래스입니다.
/// </summary>
public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    /// <summary>
    /// Cinemachine의 충격 소스를 저장하는 변수입니다.
    /// </summary>
    private CinemachineImpulseSource source;

    /// <summary>
    /// 싱글톤 인스턴스가 초기화될 때 호출됩니다.
    /// </summary>
    protected override void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>(); // CinemachineImpulseSource 컴포넌트를 가져옴
        
        base.Awake(); // 싱글톤 패턴에서 기본 Awake 실행
    }

    /// <summary>
    /// 화면 흔들림 효과를 실행하는 메서드입니다.
    /// </summary>
    public void ShakeScreen()
    {
        source.GenerateImpulse(); // 충격을 발생시켜 카메라 흔들림 효과 적용
    }
}
