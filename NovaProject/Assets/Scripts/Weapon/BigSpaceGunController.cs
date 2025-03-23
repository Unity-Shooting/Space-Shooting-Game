using UnityEngine;

/// <summary>
/// ���� ������(BigSpaceGun)�� �����ϴ� ��Ʈ�ѷ� Ŭ����.
/// IWeaponController �������̽��� �����Ѵ�.
/// </summary>
public class BigSpaceGunController : MonoBehaviour, IWeaponController
{
    /// <summary>
    /// �߻�� źȯ ������.
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// �߻� ��ġ.
    /// </summary>
    public Transform launcher1;

    /// <summary>
    /// Unity�� �⺻ Start �޼��� (����� ������ ����).
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� Unity�� �⺻ Update �޼��� (����� ������ ����).
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// ���� �������� �߻��ϴ� �޼���.
    /// </summary>
    public void Shooting()
    {
        // TODO: �ִϸ��̼� ��� ���� �߰�
        Instantiate(bullet, launcher1.position, PlayerController.Instance.transform.rotation); // �߻� ��ġ���� źȯ ����
    }
}
