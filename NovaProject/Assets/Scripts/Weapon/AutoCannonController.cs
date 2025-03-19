using UnityEngine;

/// <summary>
/// �ڵ� ����(AutoCannon)�� �����ϴ� ��Ʈ�ѷ� Ŭ����.
/// IWeaponController �������̽��� �����Ѵ�.
/// </summary>
public class AutoCannonController : MonoBehaviour, IWeaponController
{
    /// <summary>
    /// �߻�� źȯ ������.
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// ù ��° �߻� ��ġ.
    /// </summary>
    public Transform launcher1;

    /// <summary>
    /// �� ��° �߻� ��ġ.
    /// </summary>
    public Transform launcher2;

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
    /// �ڵ� ������ �߻��ϴ� �޼���.
    /// </summary>
    public void Shooting()
    {
        // TODO: �߻� �ִϸ��̼� ��� ���� �߰�
        Instantiate(bullet, launcher1.position, Quaternion.identity); // ù ��° �߻� ��ġ���� źȯ ����
        Instantiate(bullet, launcher2.position, Quaternion.identity); // �� ��° �߻� ��ġ���� źȯ ����
    }
}
