using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// ȭ�� ��鸲 ȿ���� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    /// <summary>
    /// Cinemachine�� ��� �ҽ��� �����ϴ� �����Դϴ�.
    /// </summary>
    private CinemachineImpulseSource source;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �ʱ�ȭ�� �� ȣ��˴ϴ�.
    /// </summary>
    protected override void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>(); // CinemachineImpulseSource ������Ʈ�� ������
        
        base.Awake(); // �̱��� ���Ͽ��� �⺻ Awake ����
    }

    /// <summary>
    /// ȭ�� ��鸲 ȿ���� �����ϴ� �޼����Դϴ�.
    /// </summary>
    public void ShakeScreen()
    {
        source.GenerateImpulse(); // ����� �߻����� ī�޶� ��鸲 ȿ�� ����
    }
}
