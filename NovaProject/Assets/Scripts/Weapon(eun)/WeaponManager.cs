using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;


namespace WeaponEun
{

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
        public GameObject E_Skill, F_Skill, A_Skill;

        // �Ѿ� �߻� ��ġ (Player�� �߻� ��ġ)
        public Transform firePoint1, firePoint2, firePoint3;

        public int power = 0;
        private GameObject powerup;
        
        // Rocket ���� ����
        private bool isRocket = false;
        private float rocketTime = 4f; // ���� �߻� ���� �ð� (��)
        private float rocketCool = 0.3f; // �߻� ���� (��)
        private float rocketTimer = 0f; // �߻� Ÿ�̸�

        // Missile ���� ����
        private bool isMissile = false; // Missile ��ų Ȱ��ȭ ����
        private float missileTime = 4f; // Missile �߻� ���� �ð� (��)
        private float missileCool = 0.3f; // Missile �߻� ���� (��)
        private float missileTimer = 0f; // Missile �߻� Ÿ�̸�


        private bool isSkillOnCooldown = false; // ��ų ��Ÿ�� ����
        private float skillCooldown = 10f; // ��ų ��Ÿ��
        private float skillCooldownTimer = 0f; // ��ų ��Ÿ�� Ÿ�̸�



        // ���׷��̵� �޼ҵ�
        public void UpgradeWeapon()
        {
            power += 1;
            if (power > 2)
                power = 2;

            //�Ŀ��� �޼��� ���
            /*GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
            Destroy(go, 1);*/
        }

        // �Ѿ� �߻� �޼ҵ�
        public void Fire()
        {
            PBullet bulletScript1 = null; // PBullet ���� ���� �ʱ�ȭ
            PBullet bulletScript2 = null; // �� ��° �Ѿ˿� ���� PBullet ���� ���� �ʱ�ȭ
            switch (currentWeapon)
            {
                case WeaponType.ElectroGun:
                    // ù ��° �Ѿ� �߻�
                    GameObject eBullet1 = Instantiate(E_Bullet[power], firePoint1.position, firePoint1.rotation);
                    bulletScript1 = eBullet1.GetComponent<PBullet>();
                    if (bulletScript1 != null)
                    {
                        bulletScript1.isHoming = true; // ElectroGun�� Ÿ���� Ȱ��ȭ
                    }

                    // �� ��° �Ѿ� �߻�
                    GameObject eBullet2 = Instantiate(E_Bullet[power], firePoint2.position, firePoint2.rotation);
                    bulletScript2 = eBullet2.GetComponent<PBullet>();
                    if (bulletScript2 != null)
                    {
                        bulletScript2.isHoming = true; // ElectroGun�� Ÿ���� Ȱ��ȭ
                    }
                    break;

                case WeaponType.FlameCannon:
                    Instantiate(F_Bullet[power], firePoint1.position, firePoint1.rotation);
                    Instantiate(F_Bullet[power], firePoint2.position, firePoint2.rotation);
                    break;

                case WeaponType.AquaCannon:
                    Instantiate(A_Bullet[power], firePoint1.position, firePoint1.rotation);
                    Instantiate(A_Bullet[power], firePoint2.position, firePoint2.rotation);
                    break;

                default:
                    Debug.LogError("�� �� ���� ���� �����Դϴ�.");
                    return;

            }
        }



        // ��ų �߻� �޼ҵ�
        public void FireSkill()
        {
            if (isSkillOnCooldown)
            {
                Debug.Log("��ų�� ��Ÿ�� ���Դϴ�.");
                return;
            }

            isSkillOnCooldown = true; // ��ų ��Ÿ�� ����
            skillCooldownTimer = skillCooldown; // ��Ÿ�� Ÿ�̸� ����

            switch (currentWeapon)
            {
                case WeaponType.ElectroGun:
                    Instantiate(E_Skill, firePoint3.position, firePoint3.rotation);
                    break;

                case WeaponType.FlameCannon:
                    isRocket = true;
                    rocketTime = 4f;
                    rocketTimer = rocketCool;
                    break;

                case WeaponType.AquaCannon:
                    isMissile = true; // Missile ��ų Ȱ��ȭ
                    missileTime = 4f; // �߻� ���� �ð� ����
                    missileTimer = missileCool; // �߻� ���� ����
                    break;

                default:
                    Debug.LogError("�߸��� �����Դϴ�.");
                    break;
            }
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
            // BŰ�� ��ų ��� (WeaponManager �ȿ��� ó��)
            if (Input.GetKeyDown(KeyCode.B))
            {
                FireSkill();
            }

            // �����̽��ٷ� �⺻ �Ѿ� �߻�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }

            // 1, 2, 3 Ű�� ���� ����
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchWeapon(3);
            }

            // ��ų ��Ÿ�� Ÿ�̸� ������Ʈ
            if (isSkillOnCooldown)
            {
                skillCooldownTimer -= Time.deltaTime;
                if (skillCooldownTimer <= 0f)
                {
                    isSkillOnCooldown = false;
                }
            }


            // ���� ��ų ó��
            if (isRocket)
            {
                rocketTimer -= Time.deltaTime;

                if (rocketTimer <= 0f)
                {
                    Instantiate(F_Skill, firePoint3.position, firePoint3.rotation);
                    rocketTimer = rocketCool;
                }

                rocketTime -= Time.deltaTime;
                if (rocketTime <= 0f)
                {
                    isRocket = false;
                }
            }

            // Missile ��ų ó��
            if (isMissile)
            {
                missileTimer -= Time.deltaTime;

                if (missileTimer <= 0f)
                {
                    Instantiate(A_Skill, firePoint3.position, firePoint3.rotation); // Missile �߻�
                    missileTimer = missileCool; // Ÿ�̸� ����
                }

                missileTime -= Time.deltaTime;
                if (missileTime <= 0f)
                {
                    isMissile = false; // Missile ��ų ����
                }
            }

        }

    }
}