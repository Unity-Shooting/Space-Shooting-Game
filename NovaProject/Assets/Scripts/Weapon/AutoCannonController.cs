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
    /// ����ĳ�� �ִϸ�����.
    /// </summary>
    private Animator am;

    /// <summary>
    /// ù ��° �߻� ��ġ.
    /// </summary>
    public Transform launcher1;

    /// <summary>
    /// �� ��° �߻� ��ġ.
    /// </summary>
    public Transform launcher2;

    /// <summary>
    /// Unity�� �⺻ Awake �޼��� 
    /// </summary>
    void Awake()
    {
        am = GetComponent<Animator>();
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
        am.SetTrigger("shoot");
        SFXManager.Instance.ShootSound();
        Instantiate(bullet, launcher1.position, PlayerController.Instance.transform.rotation); // ù ��° �߻� ��ġ���� źȯ ����
        Instantiate(bullet, launcher2.position, PlayerController.Instance.transform.rotation); // �� ��° �߻� ��ġ���� źȯ ����
    }
}
