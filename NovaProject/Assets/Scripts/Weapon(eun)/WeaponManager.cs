using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;


// ���� ������ �����ϴ� ������
public enum weaponType
{
    // �ӽ� ���� �̸�
    Laser,
    Rocket,
    Missile

}

public class WeaponManager : MonoBehaviour
{
    // ���� ������ ����
    public weaponType currentWeapon;

    // �Ѿ� ������
    public GameObject[] Bullet_1, Bullet_2, Bullet_3;

    // ��ų ������
    public GameObject Skill_1, Skill_2, Skill_3;

    // �Ѿ� �߻� ��ġ (Player�� �߻� ��ġ)
    public Transform firePoint1, firePoint2, firePoint3;

    // �Ѿ� �Ŀ�(���׷��̵� �ܰ�)
    public int power = 0;
    private GameObject powerup;

    // Rocket ���� ����
    private bool isRocket = false; // Rocket ��ų Ȱ��ȭ ����
    private float rocketTime = 4f; // ���� �߻� ���� �ð� (��)
    private float rocketCool = 0.3f; // �߻� ���� (��)
    private float rocketTimer = 0f; // �߻� Ÿ�̸�

    // Missile ���� ����
    private bool isMissile = false; // Missile ��ų Ȱ��ȭ ����
    private float missileTime = 4f; // Missile �߻� ���� �ð� (��)
    private float missileCool = 0.3f; // Missile �߻� ���� (��)
    private float missileTimer = 0f; // Missile �߻� Ÿ�̸�

    // ��ų ��Ÿ�� ����
    private bool isSkillOnCooldown = false; // ��ų ��Ÿ�� ����
    private float skillCooldown = 10f; // ��ų ��Ÿ��
    private float skillCooldownTimer = 0f; // ��ų ��Ÿ�� Ÿ�̸�



    // ���� �����յ��� ������ ����
    public GameObject[] weaPrefabs; // ���� ������ �迭
    private int currentWeaIndex = 0; // ���� Ȱ��ȭ�� ������ �ε���

    // ���� �����յ��� ������ ����
    public GameObject[] enginePrefabs; // ���� ������ �迭
    private int currentEngineIndex = 0; // ���� Ȱ��ȭ�� ������ �ε���

    public static WeaponManager Instance; // �̱��� �ν��Ͻ�

    // ��ų �ر� ����
    public bool LaserUnlocked { get; private set; } = false;
    public bool RocketUnlocked { get; private set; } = false;
    public bool MissileUnlocked { get; private set; } = false;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    // ��ų �������� ������ �ر��ϴ� �޼���
    public void UnlockSkill(string skillName)
    {
        switch (skillName)
        {
            case "Laser":
                LaserUnlocked = true;
                Debug.Log("Laser ��ų �رݵ�!");
                break;
            case "Rocket":
                RocketUnlocked = true;
                Debug.Log("Rocket ��ų �رݵ�!");
                break;
            case "Missile":
                MissileUnlocked = true;
                Debug.Log("Missile ��ų �رݵ�!");
                break;
            default:
                Debug.LogError("�� �� ���� ��ų �̸�: " + skillName);
                break;
        }
    }

    // ��ų ��� ���� Ȯ�� (����)
    public bool CanUseSkill(weaponType weaponType)
    {
        switch (weaponType)
        {
            case weaponType.Laser:
                return LaserUnlocked;
            case weaponType.Rocket:
                return RocketUnlocked;
            case weaponType.Missile:
                return MissileUnlocked;
            default:
                return false;
        }
    }


    // ���� ����� �����ϴ� �Լ�
    public void ChangeWeapon()
    {
        // ���� ������ ��Ȱ��ȭ
        weaPrefabs[currentWeaIndex].SetActive(false);

        // ���ο� ���� �ε����� ����
        currentWeaIndex = (currentWeaIndex + 1) % weaPrefabs.Length;

        // ���ο� ������ Ȱ��ȭ
        SetWeaponActive(currentWeaIndex);
    }

    // ���⸦ ������� �ǵ����� �Լ�
    public void ResetWeapon()
    {
        // ���� ���⸦ ��Ȱ��ȭ
        weaPrefabs[currentWeaIndex].SetActive(false);

        // ���� �ε����� 0���� �ʱ�ȭ�Ͽ� �⺻ ������ Ȱ��ȭ
        currentWeaIndex = 0;

        // �⺻ ���⸦ Ȱ��ȭ
        SetWeaponActive(currentWeaIndex);
    }

    // ������ �ε����� ���⸦ Ȱ��ȭ/��Ȱ��ȭ�ϴ� �Լ�
    private void SetWeaponActive(int index)
    {
        if (index >= 0 && index < weaPrefabs.Length)
        {
            weaPrefabs[index].SetActive(true);
        }
    }

    // ���� ����� �����ϴ� �Լ�
    public void ChangeEngine()
    {
        // ���� ������ ��Ȱ��ȭ
        enginePrefabs[currentEngineIndex].SetActive(false);

        // ���ο� ���� �ε����� ����
        currentEngineIndex = (currentEngineIndex + 1) % enginePrefabs.Length;

        // ���ο� ������ Ȱ��ȭ
        SetEngineActive(currentEngineIndex);
    }

    // ������ ������� �ǵ����� �Լ�
    public void ResetEngine()
    {
        // ���� ������ ��Ȱ��ȭ
        enginePrefabs[currentEngineIndex].SetActive(false);

        // ���� �ε����� 0���� �ʱ�ȭ�Ͽ� �⺻ ������ Ȱ��ȭ
        currentEngineIndex = 0;

        // �⺻ ������ Ȱ��ȭ
        SetEngineActive(currentEngineIndex);
    }

    // ������ �ε����� ������ Ȱ��ȭ/��Ȱ��ȭ�ϴ� �Լ�
    private void SetEngineActive(int index)
    {
        if (index >= 0 && index < enginePrefabs.Length)
        {
            enginePrefabs[index].SetActive(true);
        }
    }




    // �Ѿ� ���׷��̵� �޼ҵ�
    public void UpgradeWeapon()
    {
        power += 1;
        if (power > 2)
            power = 2;

        Debug.Log("power up");

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
            case weaponType.Laser:
                // ù ��° �Ѿ� �߻�
                GameObject eBullet1 = Instantiate(Bullet_1[power], firePoint1.position, firePoint1.rotation);
                bulletScript1 = eBullet1.GetComponent<PBullet>();
                if (bulletScript1 != null)
                {
                    bulletScript1.isHoming = true; // ElectroGun�� Ÿ���� Ȱ��ȭ
                }

                // �� ��° �Ѿ� �߻�
                GameObject eBullet2 = Instantiate(Bullet_1[power], firePoint2.position, firePoint2.rotation);
                bulletScript2 = eBullet2.GetComponent<PBullet>();
                if (bulletScript2 != null)
                {
                    bulletScript2.isHoming = true; // ElectroGun�� Ÿ���� Ȱ��ȭ
                }

                SFXManager.Instance.ShootSound();

                break;


            case weaponType.Rocket:
                Instantiate(Bullet_2[power], firePoint1.position, firePoint1.rotation);
                Instantiate(Bullet_2[power], firePoint2.position, firePoint2.rotation);

                SFXManager.Instance.ShootSound();

                break;

            case weaponType.Missile:
                Instantiate(Bullet_3[power], firePoint1.position, firePoint1.rotation);
                Instantiate(Bullet_3[power], firePoint2.position, firePoint2.rotation);

                SFXManager.Instance.ShootSound();

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

        // ��ų �ر� ���� Ȯ��
        if (!CanUseSkill(currentWeapon))
        {
            Debug.Log($"{currentWeapon} ��ų�� �رݵ��� �ʾҽ��ϴ�.");
            return;
        }

        isSkillOnCooldown = true; // ��ų ��Ÿ�� ����
        skillCooldownTimer = skillCooldown; // ��Ÿ�� Ÿ�̸� ����

        switch (currentWeapon)
        {
            case weaponType.Laser:
                Instantiate(Skill_1, firePoint1.position, firePoint1.rotation);
                break;

            case weaponType.Rocket:
                isRocket = true; // rocket ��ų Ȱ��ȭ
                rocketTime = 4f; // ���� �ð� ����
                rocketTimer = rocketCool; // �߻� ���� ����
                break;

            case weaponType.Missile:
                isMissile = true; // Missile ��ų Ȱ��ȭ
                missileTime = 4f; // ���� �ð� ����
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
                currentWeapon = weaponType.Laser;
                Debug.Log("Laser ������");
                break;
            case 2:
                currentWeapon = weaponType.Rocket;
                Debug.Log("Rocket ������");
                break;
            case 3:
                currentWeapon = weaponType.Missile;
                Debug.Log("Missile ������");
                break;
            default:
                Debug.LogError("�߸��� ���� �ε���: " + weaponIndex);
                break;
        }

        // ����� ������ �����Ϸ��� �̰����� ������ ���� ���¸� �����ϵ��� ����
        // Reset�� ���� ����� �⺻ ���·� �ǵ��ư���, �� �Ŀ� ����
        ResetEngine(); // ���� ���� �ʱ�ȭ
        ResetWeapon(); // ���� ���� �ʱ�ȭ

        if (weaponIndex == 2)
        {
            // Laser ������ ��� ������ ���⸦ ����
            ChangeEngine();
            ChangeWeapon();
        }
        else if (weaponIndex == 1)
        {
            // AutoCannon ������ ��� ������ ���⸦ ����
            ChangeEngine();
            ChangeWeapon();
        }
        else if (weaponIndex == 3)
        {
            // test ������ ��� ������ ���⸦ ����
            ChangeEngine();
            ChangeWeapon();
        }

    }


    void Start()
    {
        // ���� �� ù ��° ������ Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ
        SetEngineActive(currentEngineIndex);
    }

    void Update()
    {
        // BŰ�� ��ų ��� 
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
            skillCooldownTimer -= Time.deltaTime; // �����Ӵ� ��� �ð��� ��Ÿ�� Ÿ�̸ӿ��� ����
            if (skillCooldownTimer <= 0f)
            {
                isSkillOnCooldown = false; // ��Ÿ���� �������� ǥ��
            }
        }


        // ���� ��ų ó��
        if (isRocket)
        {
            rocketTimer -= Time.deltaTime; // ���� �߻� ���� Ÿ�̸� ������Ʈ

            if (rocketTimer <= 0f)
            {
                Instantiate(Skill_2, firePoint3.position, firePoint3.rotation); // ���� �߻�
                rocketTimer = rocketCool; // �߻� ���� Ÿ�̸� ����
            }

            rocketTime -= Time.deltaTime; // ���� ��ų�� ��ü ���ӽð� ������Ʈ
            if (rocketTime <= 0f)
            {
                isRocket = false; // ���� ��ų ����
            }
        }

        // Missile ��ų ó��
        if (isMissile)
        {
            missileTimer -= Time.deltaTime; // �̻��� �߻� ���� Ÿ�̸� ������Ʈ

            if (missileTimer <= 0f)
            {
                Instantiate(Skill_3, firePoint3.position, firePoint3.rotation); // Missile �߻�
                missileTimer = missileCool; // Ÿ�̸� ����
            }

            missileTime -= Time.deltaTime; // �̻��� ��ų�� ��ü ���ӽð� ������Ʈ
            if (missileTime <= 0f)
            {
                isMissile = false; // Missile ��ų ����
            }
        }

    }

}