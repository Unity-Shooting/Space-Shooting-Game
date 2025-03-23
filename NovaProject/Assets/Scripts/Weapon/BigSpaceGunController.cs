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
    /// ����ĳ�� �ִϸ�����.
    /// </summary>
    private Animator am;

    /// <summary>
    /// �߻� ��ġ.
    /// </summary>
    public Transform launcher1;

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
    /// ���� �������� �߻��ϴ� �޼���.
    /// </summary>
    public void Shooting()
    {
        am.SetTrigger("shoot");
    }

    public void delayShoot()
    {
        SoundEffectManager.Instance.ShootSound();
        Instantiate(bullet, launcher1.position, PlayerController.Instance.transform.rotation); // �߻� ��ġ���� źȯ ����
    }
}
