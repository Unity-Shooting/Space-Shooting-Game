using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SceneManagement;

// 무기 유형을 정의하는 열거형
public enum weaponType
{
    // 임시 무기 이름
    Missile,
    Laser,
    Bomb

}

public class WeaponManager : MonoBehaviour
{
    // 현재 장착된 무기
    public weaponType currentWeapon;

    // 총알 프리팹
    public GameObject[] Bullet_1, Bullet_2, Bullet_3;
    public GameObject Bullet_A, Bullet_B, Bullet_C; // StageHidden에서 사용할 총알


    // 스킬 프리팹
    public GameObject Skill_1, Skill_2, Skill_3;

    // 총알 발사 위치 (Player의 발사 위치)
    public Transform firePoint1, firePoint2, firePoint3;

    // 총알 파워(업그레이드 단계)
    public int power = 0;
    private GameObject powerup;

    // Missile 관련 변수
    private bool isMissile = false; // Missile 스킬 활성화 여부
    private float missileTime = 4f; // Missile 발사 지속 시간 (초)
    private float missileCool = 0.3f; // Missile 발사 간격 (초)
    private float missileTimer = 0f; // Missile 발사 타이머

    // Bomb 관련 변수
    private bool isBomb = false; // Rocket 스킬 활성화 여부
    private float bombTime = 4f; // 로켓 발사 지속 시간 (초)
    private float bombCool = 0.3f; // 발사 간격 (초)
    private float bombTimer = 0f; // 발사 타이머

    // Laser 관련 변수
    private bool isLaser = false; // Laser 스킬 활성화 여부
    private float laserTime = 4f; // Laser 지속 시간 (초)
    private GameObject laser1, laser2; // 레이저 오브젝트



    // 스킬 쿨타임 관리
    private bool isSkillOnCooldown = false; // 스킬 쿨타임 여부
    private float skillCool = 10f; // 스킬 쿨타임
    private float skillCoolTimer = 0f; // 스킬 쿨타임 타이머

    public Animator Zapper;
    public Animator Missile;
    public Animator Bomb;


    // 무기 프리팹 배열
    public GameObject[] weaponPrefabs;

    // 엔진 프리팹 배열
    public GameObject[] enginePrefabs;

    // 현재 무기 및 엔진 상태
    private int currentWeaponIndex = -1; // 선택되지 않은 상태로 초기화
    private int currentEngineIndex = -1; // 선택되지 않은 상태로 초기화

    public static WeaponManager Instance; // 싱글톤 인스턴스

    // 스킬 해금 여부
    public bool MissileUnlocked { get; private set; } = false;
    public bool LaserUnlocked { get; private set; } = false;
    public bool BombUnlocked { get; private set; } = false;


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
            case "Missile":
                MissileUnlocked = true;
                Debug.Log("Missile 스킬 해금됨!");
                break;
            case "Laser":
                LaserUnlocked = true;
                Debug.Log("Laser 스킬 해금됨!");
                break;
            case "Bomb":
                BombUnlocked = true;
                Debug.Log("Bomb 스킬 해금됨!");
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
            case weaponType.Missile:
                return MissileUnlocked;
            case weaponType.Laser:
                return LaserUnlocked;
            case weaponType.Bomb:
                return BombUnlocked;
            default:
                return false;
        }
    }

    // 무기 및 엔진 활성화/비활성화 메서드
    private void SetActiveState(GameObject[] prefabs, int activeIndex)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i].SetActive(i == activeIndex);
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
                                      // 현재 씬이 "StageHidden"인지 체크
        if (SceneManager.GetActiveScene().name == "StageHidden")
        {
            // "StageHidden" 씬일 때, 각 무기에 맞는 특수 총알을 발사
            switch (currentWeapon)
            {
                case weaponType.Missile:
                    GameObject aBullet = Instantiate(Bullet_A, firePoint3.position, firePoint3.rotation);
                    bulletScript1 = aBullet.GetComponent<PBullet>();
                    if (bulletScript1 != null)
                    {
                        bulletScript1.isHoming = true;
                    }

                    SFXManager.Instance.ShootSound();
                    break;

                case weaponType.Laser:
                    Zapper.SetTrigger("shoot");
                    GameObject bBullet = Instantiate(Bullet_B, firePoint3.position, firePoint3.rotation);
                    bulletScript1 = bBullet.GetComponent<PBullet>();
                    if (bulletScript1 != null)
                    {
                        bulletScript1.isHoming = true;
                    }

                    SFXManager.Instance.ShootSound();
                    break;

                case weaponType.Bomb:
                    Bomb.SetTrigger("shoot");
                    GameObject cBullet = Instantiate(Bullet_C, firePoint3.position, firePoint3.rotation);
                    bulletScript1 = cBullet.GetComponent<PBullet>();
                    if (bulletScript1 != null)
                    {
                        bulletScript1.isHoming = true;
                    }

                    SFXManager.Instance.ShootSound();
                    break;

                default:
                    Debug.LogError("알 수 없는 무기 유형입니다.");
                    return;
            }
        }
        else
        {
            switch (currentWeapon)
            {
                case weaponType.Missile:

                    if (Missile != null)
                    {
                        Missile.SetTrigger("shoot");
                    }
                    else
                    {
                        Debug.LogError("Missile Null");
                    }

                    // 첫 번째 총알 발사
                    GameObject eBullet1 = Instantiate(Bullet_1[power], firePoint1.position, firePoint1.rotation);
                    bulletScript1 = eBullet1.GetComponent<PBullet>();
                    if (bulletScript1 != null)
                    {
                        bulletScript1.isHoming = true; // Missile만 타겟팅 활성화
                    }

                    // 두 번째 총알 발사
                    GameObject eBullet2 = Instantiate(Bullet_1[power], firePoint2.position, firePoint2.rotation);
                    bulletScript2 = eBullet2.GetComponent<PBullet>();
                    if (bulletScript2 != null)
                    {
                        bulletScript2.isHoming = true; //Missile만 타겟팅 활성화
                    }


                    SFXManager.Instance.ShootSound();

                    break;


                case weaponType.Laser:

                    if (Zapper != null)
                    {
                        Zapper.SetTrigger("shoot");
                    }
                    else
                    {
                        Debug.LogError("Zapper Null");
                    }


                    Instantiate(Bullet_2[power], firePoint1.position, firePoint1.rotation);
                    Instantiate(Bullet_2[power], firePoint2.position, firePoint2.rotation);

                    SFXManager.Instance.ShootSound();

                    break;

                case weaponType.Bomb:

                    if (Bomb != null)
                    {
                        Bomb.SetTrigger("shoot");
                    }
                    else
                    {
                        Debug.LogError("Bomb Null");
                    }

                    Instantiate(Bullet_3[power], firePoint1.position, firePoint1.rotation);
                    Instantiate(Bullet_3[power], firePoint2.position, firePoint2.rotation);

                    SFXManager.Instance.ShootSound();

                    break;


                default:
                    Debug.LogError("알 수 없는 무기 유형입니다.");
                    return;
            }
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
        skillCoolTimer = skillCool; // 쿨타임 타이머 설정

        switch (currentWeapon)
        {
            case weaponType.Missile:
                isMissile = true; // Missile 스킬 활성화
                missileTime = 4f; // 지속 시간 설정
                missileTimer = missileCool; // 발사 간격 설정
                break;

            case weaponType.Laser:
                isLaser = true;
                laserTime = 4f;

                // 레이저 1과 레이저 2 생성 후 클래스 변수에 할당
                laser1 = Instantiate(Skill_2, firePoint1.position + (Vector3.up * 5.5f), firePoint1.rotation);
                laser2 = Instantiate(Skill_2, firePoint2.position + (Vector3.up * 5.5f), firePoint2.rotation);
                SFXManager.Instance.ShootSound();

                // Laser 스크립트를 가져와서 설정
                Laser laser1Script = laser1.GetComponent<Laser>();
                Laser laser2Script = laser2.GetComponent<Laser>();

                if (laser1Script != null)
                {
                    laser1Script.SetLaserIdentifier("laser1");
                    laser1Script.firePoint1 = firePoint1;
                }

                if (laser2Script != null)
                {
                    laser2Script.SetLaserIdentifier("laser2");
                    laser2Script.firePoint2 = firePoint2;
                }

                break;

            case weaponType.Bomb:
                isBomb = true; // Bomb 스킬 활성화
                bombTime = 4f; // 지속 시간 설정
                bombTimer = missileCool; // 발사 간격 설정
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
                currentWeapon = weaponType.Missile;
                Debug.Log("Missile 장착됨");
                break;
            case 2:
                currentWeapon = weaponType.Laser;
                Debug.Log("Laser 장착됨");
                break;
            case 3:
                currentWeapon = weaponType.Bomb;
                Debug.Log("Bomb 장착됨");
                break;
            default:
                Debug.LogError("잘못된 무기 인덱스: " + weaponIndex);
                break;

        }

        if (currentWeaponIndex == weaponIndex - 1)
        {
            Debug.Log($"현재 {currentWeapon} 상태입니다.");
            return;
        }

        currentWeaponIndex = weaponIndex - 1;
        currentEngineIndex = currentWeaponIndex;  // 엔진 인덱스도 같이 변경
        currentWeapon = (weaponType)currentWeaponIndex;

        // 무기 및 엔진 상태를 업데이트
        SetActiveState(weaponPrefabs, currentWeaponIndex);
        SetActiveState(enginePrefabs, currentEngineIndex); // 엔진도 변경

        Debug.Log($"{currentWeapon} 무기와 관련 엔진이 활성화되었습니다.");

    }


    void Start()
    {
        // 초기 상태 설정 (모든 무기와 엔진 비활성화)
        SetActiveState(weaponPrefabs, -1);
        SetActiveState(enginePrefabs, -1);

        // 기본 무기 장착 (1번 무기)
        SwitchWeapon(1);
    }

    void Update()
    {
        // 현재 씬이 "StageHidden"일 경우 B키로 스킬 사용 불가
        if (SceneManager.GetActiveScene().name != "StageHidden")
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                FireSkill();
            }
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
            skillCoolTimer -= Time.deltaTime; // 프레임당 경과 시간을 쿨타임 타이머에서 차감
            if (skillCoolTimer <= 0f)
            {
                isSkillOnCooldown = false; // 쿨타임이 끝났음을 표시
            }
        }

        // Missile 스킬 처리
        if (isMissile)
        {
            Debug.Log("firePoint1 Position: " + firePoint1.position);
            Debug.Log("firePoint2 Position: " + firePoint2.position);

            missileTimer -= Time.deltaTime; // 미사일 발사 간격 타이머 업데이트

            if (missileTimer <= 0f)
            {
                Instantiate(Skill_1, firePoint1.position, firePoint1.rotation); // Missile 발사
                Instantiate(Skill_1, firePoint2.position, firePoint2.rotation);
                SFXManager.Instance.ShootSound();
                missileTimer = missileCool; // 타이머 리셋
            }

            missileTime -= Time.deltaTime; // 미사일 스킬의 전체 지속시간 업데이트
            if (missileTime <= 0f)
            {
                isMissile = false; // Missile 스킬 종료
            }
        }

        // Bomb 스킬 처리
        if (isBomb)
        {
            bombTimer -= Time.deltaTime; // 미사일 발사 간격 타이머 업데이트

            if (bombTimer <= 0f)
            {
                Instantiate(Skill_3, firePoint3.position, firePoint3.rotation); // Missile 발사
                SFXManager.Instance.ShootSound();
                bombTimer = bombCool; // 타이머 리셋
            }

            bombTime -= Time.deltaTime; // 미사일 스킬의 전체 지속시간 업데이트
            if (bombTime <= 0f)
            {
                isBomb = false; // Missile 스킬 종료
            }
        }

        if (isLaser)
        {
            // 지속 시간 감소
            laserTime -= Time.deltaTime;

            if (laserTime <= 0f)
            {
                // 레이저 스킬 종료
                isLaser = false;

                // 레이저 오브젝트 비활성화
                Debug.Log("레이저 종료: " + laser1 + ", " + laser2); // 레이저가 올바르게 할당되었는지 확인

                Destroy(laser1);
                Destroy(laser2);
            }
        }
    }

}