using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;


// 무기 유형을 정의하는 열거형
public enum weaponType
{
    // 임시 무기 이름
    Laser,
    Rocket,
    Missile

}

public class WeaponManager : MonoBehaviour
{
    // 현재 장착된 무기
    public weaponType currentWeapon;

    // 총알 프리팹
    public GameObject[] Bullet_1, Bullet_2, Bullet_3;

    // 스킬 프리팹
    public GameObject Skill_1, Skill_2, Skill_3;

    // 총알 발사 위치 (Player의 발사 위치)
    public Transform firePoint1, firePoint2, firePoint3;

    // 총알 파워(업그레이드 단계)
    public int power = 0;
    private GameObject powerup;

    // Rocket 관련 변수
    private bool isRocket = false; // Rocket 스킬 활성화 여부
    private float rocketTime = 4f; // 로켓 발사 지속 시간 (초)
    private float rocketCool = 0.3f; // 발사 간격 (초)
    private float rocketTimer = 0f; // 발사 타이머

    // Missile 관련 변수
    private bool isMissile = false; // Missile 스킬 활성화 여부
    private float missileTime = 4f; // Missile 발사 지속 시간 (초)
    private float missileCool = 0.3f; // Missile 발사 간격 (초)
    private float missileTimer = 0f; // Missile 발사 타이머

    // 스킬 쿨타임 관리
    private bool isSkillOnCooldown = false; // 스킬 쿨타임 여부
    private float skillCooldown = 10f; // 스킬 쿨타임
    private float skillCooldownTimer = 0f; // 스킬 쿨타임 타이머



    // 무기 프리팹들을 저장할 변수
    public GameObject[] weaPrefabs; // 엔진 프리팹 배열
    private int currentWeaIndex = 0; // 현재 활성화된 엔진의 인덱스

    // 엔진 프리팹들을 저장할 변수
    public GameObject[] enginePrefabs; // 엔진 프리팹 배열
    private int currentEngineIndex = 0; // 현재 활성화된 엔진의 인덱스

    public static WeaponManager Instance; // 싱글톤 인스턴스

    // 스킬 해금 여부
    public bool LaserUnlocked { get; private set; } = false;
    public bool RocketUnlocked { get; private set; } = false;
    public bool MissileUnlocked { get; private set; } = false;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    // 스킬 아이템을 먹으면 해금하는 메서드
    public void UnlockSkill(string skillName)
    {
        switch (skillName)
        {
            case "Laser":
                LaserUnlocked = true;
                Debug.Log("Laser 스킬 해금됨!");
                break;
            case "Rocket":
                RocketUnlocked = true;
                Debug.Log("Rocket 스킬 해금됨!");
                break;
            case "Missile":
                MissileUnlocked = true;
                Debug.Log("Missile 스킬 해금됨!");
                break;
            default:
                Debug.LogError("알 수 없는 스킬 이름: " + skillName);
                break;
        }
    }

    // 스킬 사용 조건 확인 (예시)
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


    // 무기 모양을 변경하는 함수
    public void ChangeWeapon()
    {
        // 현재 엔진을 비활성화
        weaPrefabs[currentWeaIndex].SetActive(false);

        // 새로운 엔진 인덱스를 설정
        currentWeaIndex = (currentWeaIndex + 1) % weaPrefabs.Length;

        // 새로운 엔진을 활성화
        SetWeaponActive(currentWeaIndex);
    }

    // 무기를 원래대로 되돌리는 함수
    public void ResetWeapon()
    {
        // 현재 무기를 비활성화
        weaPrefabs[currentWeaIndex].SetActive(false);

        // 무기 인덱스를 0으로 초기화하여 기본 엔진을 활성화
        currentWeaIndex = 0;

        // 기본 무기를 활성화
        SetWeaponActive(currentWeaIndex);
    }

    // 지정된 인덱스의 무기를 활성화/비활성화하는 함수
    private void SetWeaponActive(int index)
    {
        if (index >= 0 && index < weaPrefabs.Length)
        {
            weaPrefabs[index].SetActive(true);
        }
    }

    // 엔진 모양을 변경하는 함수
    public void ChangeEngine()
    {
        // 현재 엔진을 비활성화
        enginePrefabs[currentEngineIndex].SetActive(false);

        // 새로운 엔진 인덱스를 설정
        currentEngineIndex = (currentEngineIndex + 1) % enginePrefabs.Length;

        // 새로운 엔진을 활성화
        SetEngineActive(currentEngineIndex);
    }

    // 엔진을 원래대로 되돌리는 함수
    public void ResetEngine()
    {
        // 현재 엔진을 비활성화
        enginePrefabs[currentEngineIndex].SetActive(false);

        // 엔진 인덱스를 0으로 초기화하여 기본 엔진을 활성화
        currentEngineIndex = 0;

        // 기본 엔진을 활성화
        SetEngineActive(currentEngineIndex);
    }

    // 지정된 인덱스의 엔진을 활성화/비활성화하는 함수
    private void SetEngineActive(int index)
    {
        if (index >= 0 && index < enginePrefabs.Length)
        {
            enginePrefabs[index].SetActive(true);
        }
    }




    // 총알 업그레이드 메소드
    public void UpgradeWeapon()
    {
        power += 1;
        if (power > 2)
            power = 2;

        Debug.Log("power up");

        //파워업 메세지 출력
        /*GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
        Destroy(go, 1);*/
    }

    // 총알 발사 메소드
    public void Fire()
    {
        PBullet bulletScript1 = null; // PBullet 참조 변수 초기화
        PBullet bulletScript2 = null; // 두 번째 총알에 대한 PBullet 참조 변수 초기화
        switch (currentWeapon)
        {
            case weaponType.Laser:
                // 첫 번째 총알 발사
                GameObject eBullet1 = Instantiate(Bullet_1[power], firePoint1.position, firePoint1.rotation);
                bulletScript1 = eBullet1.GetComponent<PBullet>();
                if (bulletScript1 != null)
                {
                    bulletScript1.isHoming = true; // ElectroGun만 타겟팅 활성화
                }

                // 두 번째 총알 발사
                GameObject eBullet2 = Instantiate(Bullet_1[power], firePoint2.position, firePoint2.rotation);
                bulletScript2 = eBullet2.GetComponent<PBullet>();
                if (bulletScript2 != null)
                {
                    bulletScript2.isHoming = true; // ElectroGun만 타겟팅 활성화
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
                Debug.LogError("알 수 없는 무기 유형입니다.");
                return;

        }
    }


    // 스킬 발사 메소드
    public void FireSkill()
    {
        if (isSkillOnCooldown)
        {
            Debug.Log("스킬이 쿨타임 중입니다.");
            return;
        }

        // 스킬 해금 여부 확인
        if (!CanUseSkill(currentWeapon))
        {
            Debug.Log($"{currentWeapon} 스킬이 해금되지 않았습니다.");
            return;
        }

        isSkillOnCooldown = true; // 스킬 쿨타임 시작
        skillCooldownTimer = skillCooldown; // 쿨타임 타이머 설정

        switch (currentWeapon)
        {
            case weaponType.Laser:
                Instantiate(Skill_1, firePoint1.position, firePoint1.rotation);
                break;

            case weaponType.Rocket:
                isRocket = true; // rocket 스킬 활성화
                rocketTime = 4f; // 지속 시간 설정
                rocketTimer = rocketCool; // 발사 간격 설정
                break;

            case weaponType.Missile:
                isMissile = true; // Missile 스킬 활성화
                missileTime = 4f; // 지속 시간 설정
                missileTimer = missileCool; // 발사 간격 설정
                break;

            default:
                Debug.LogError("잘못된 무기입니다.");
                break;
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        // 입력된 인덱스에 따라 무기를 변경
        switch (weaponIndex)
        {
            case 1:
                currentWeapon = weaponType.Laser;
                Debug.Log("Laser 장착됨");
                break;
            case 2:
                currentWeapon = weaponType.Rocket;
                Debug.Log("Rocket 장착됨");
                break;
            case 3:
                currentWeapon = weaponType.Missile;
                Debug.Log("Missile 장착됨");
                break;
            default:
                Debug.LogError("잘못된 무기 인덱스: " + weaponIndex);
                break;
        }

        // 무기와 엔진을 변경하려면 이곳에서 엔진과 무기 상태를 저장하도록 수정
        // Reset을 먼저 해줘야 기본 상태로 되돌아가고, 그 후에 변경
        ResetEngine(); // 엔진 상태 초기화
        ResetWeapon(); // 무기 상태 초기화

        if (weaponIndex == 2)
        {
            // Laser 무기일 경우 엔진과 무기를 변경
            ChangeEngine();
            ChangeWeapon();
        }
        else if (weaponIndex == 1)
        {
            // AutoCannon 무기일 경우 엔진과 무기를 변경
            ChangeEngine();
            ChangeWeapon();
        }
        else if (weaponIndex == 3)
        {
            // test 무기일 경우 엔진과 무기를 변경
            ChangeEngine();
            ChangeWeapon();
        }

    }


    void Start()
    {
        // 시작 시 첫 번째 엔진을 활성화하고 나머지는 비활성화
        SetEngineActive(currentEngineIndex);
    }

    void Update()
    {
        // B키로 스킬 사용 
        if (Input.GetKeyDown(KeyCode.B))
        {
            FireSkill();
        }

        // 스페이스바로 기본 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // 1, 2, 3 키로 무기 변경
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

        // 스킬 쿨타임 타이머 업데이트
        if (isSkillOnCooldown)
        {
            skillCooldownTimer -= Time.deltaTime; // 프레임당 경과 시간을 쿨타임 타이머에서 차감
            if (skillCooldownTimer <= 0f)
            {
                isSkillOnCooldown = false; // 쿨타임이 끝났음을 표시
            }
        }


        // 로켓 스킬 처리
        if (isRocket)
        {
            rocketTimer -= Time.deltaTime; // 로켓 발사 간격 타이머 업데이트

            if (rocketTimer <= 0f)
            {
                Instantiate(Skill_2, firePoint3.position, firePoint3.rotation); // 로켓 발사
                rocketTimer = rocketCool; // 발사 간격 타이머 리셋
            }

            rocketTime -= Time.deltaTime; // 로켓 스킬의 전체 지속시간 업데이트
            if (rocketTime <= 0f)
            {
                isRocket = false; // 로켓 스킬 종료
            }
        }

        // Missile 스킬 처리
        if (isMissile)
        {
            missileTimer -= Time.deltaTime; // 미사일 발사 간격 타이머 업데이트

            if (missileTimer <= 0f)
            {
                Instantiate(Skill_3, firePoint3.position, firePoint3.rotation); // Missile 발사
                missileTimer = missileCool; // 타이머 리셋
            }

            missileTime -= Time.deltaTime; // 미사일 스킬의 전체 지속시간 업데이트
            if (missileTime <= 0f)
            {
                isMissile = false; // Missile 스킬 종료
            }
        }

    }

}