using UnityEngine;
using UnityEngine.UIElements;

namespace WeaponEun {

// ���� ������ �����ϴ� ������
public enum WeaponType
{
    // �ӽ� ���� �̸�
    ElectroGun, 
    FlameCannon,
    AquaCannon
}

    public class WeaponManager : MonoBehaviour
    {
        // ���� ������ ����
        public WeaponType currentWeapon;

        // �Ѿ� ������
        public GameObject E_Bullet;
        public GameObject F_Bullet;
        public GameObject A_Bullet;

        // ��ų ������
        public GameObject E_Skill;
        public GameObject F_Skill;
        public GameObject A_Skill;

        // �Ѿ� �߻� ��ġ (Player�� �߻� ��ġ)
        public Transform firePoint;

        // �Ѿ� �߻� �޼ҵ�
        public void Fire()
        {
            switch (currentWeapon)
            {
                case WeaponType.ElectroGun:
                    FireBullet(E_Bullet);
                    break;
                case WeaponType.FlameCannon:
                    FireBullet(F_Bullet);
                    break;
                case WeaponType.AquaCannon:
                    FireBullet(A_Bullet);
                    break;
                default:
                    Debug.LogError("�� �� ���� ���� ����");
                    break;
            }
        }

        // ��ų ��� �޼ҵ�
        public void UseSkill()
        {
            // ���� ������ ���� ������ ���� ��ų�� �߻�
            switch (currentWeapon)
            {
                case WeaponType.ElectroGun:
                    FireSkill(E_Skill);
                    break;
                case WeaponType.FlameCannon:
                    FireSkill(F_Skill);
                    break;
                case WeaponType.AquaCannon:
                    FireSkill(A_Skill);
                    break;
            }
        }

        // �Ѿ� �߻� �޼ҵ�
        private void FireBullet(GameObject bulletPrefab)
        {
            // �Ѿ� ����
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        // ��ų �߻� �޼ҵ�
        private void FireSkill(GameObject skillPrefab)
        {
            // ��ų ����
            Instantiate(skillPrefab, firePoint.position, firePoint.rotation);
        }

        public void SwitchWeapon(int weaponIndex)
        {
            // �Էµ� �ε����� ���� ���⸦ ����
            switch (weaponIndex)
            {
                case 1:
                    currentWeapon = WeaponType.ElectroGun;
                    Debug.Log("ElectroGun ������");
                    break;
                case 2:
                    currentWeapon = WeaponType.FlameCannon;
                    Debug.Log("FlameCannon ������");
                    break;
                case 3:
                    currentWeapon = WeaponType.AquaCannon;
                    Debug.Log("AquaCannon ������");
                    break;
                default:
                    Debug.LogError("�߸��� ���� �ε���: " + weaponIndex);
                    break;
            }
        }


        void Start()
        {

        }

        void Update()
        {
        }

    }
}
