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
        public GameObject[] E_Bullet, F_Bullet, A_Bullet;


        // ��ų ������
        public GameObject E_Skill;
        public GameObject F_Skill;
        public GameObject A_Skill;

        // �Ѿ� �߻� ��ġ (Player�� �߻� ��ġ)
        public Transform firePoint;

        public int power = 0;
        private GameObject powerup;


        // ���׷��̵� �޼ҵ�
        public void UpgradeWeapon()
        {
            power += 1;
            if (power <= 1)
                power = 1;

            //�Ŀ��� �޼��� ���
            /*GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
            Destroy(go, 1);*/
        }

        // �Ѿ� �߻� �޼ҵ�
        public void Fire()
        {

            PBullet bulletScript = null; // PBullet ���� ���� �ʱ�ȭ
            switch (currentWeapon)
            {
                case WeaponType.ElectroGun:
                    GameObject eBullet = Instantiate(E_Bullet[power], firePoint.position, firePoint.rotation);
                    bulletScript = eBullet.GetComponent<PBullet>();

                    // PBullet ��ũ��Ʈ�� ã�� isHoming ����
                    if (bulletScript != null)
                    {
                        bulletScript.isHoming = true; // ElectroGun�� Ÿ���� Ȱ��ȭ
                    }
                    break;

                case WeaponType.FlameCannon:
                    Instantiate(F_Bullet[power], firePoint.position, firePoint.rotation);
                    break;

                case WeaponType.AquaCannon:
                    Instantiate(A_Bullet[power], firePoint.position, firePoint.rotation);
                    break;

                default:
                    Debug.LogError("�� �� ���� ���� �����Դϴ�.");
                    return;
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
