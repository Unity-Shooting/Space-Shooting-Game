using UnityEngine;

/// <summary>
/// �÷��̾��� ���⸦ �����ϴ� ��Ʈ�ѷ� Ŭ����.
/// </summary>
public class WeaponController : MonoBehaviour
{
    /// <summary>
    /// �ڵ� ����(AutoCannon) ��Ʈ�ѷ�.
    /// </summary>
    public AutoCannonController autoCannonController;

    /// <summary>
    /// ���� ������(BigSpaceGun) ��Ʈ�ѷ�.
    /// </summary>
    public BigSpaceGunController bigSpaceGunController;

    /// <summary>
    /// Unity�� �⺻ Start �޼��� (����� ������ ����).
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǹ�, ���� ���� ����� ó���� ����.
    /// </summary>
    void Update()
    {
        // TODO: ���� ���� ���� �߰�
    }

    /// <summary>
    /// ���� ���õ� ���⸦ �߻��ϴ� �޼���.
    /// </summary>
    public void Shooting()
    {
        autoCannonController.Shooting(); // �ڵ� ���� �߻�
        //bigSpaceGunController.Shooting(); // ���� ������ �߻�
    }
}
