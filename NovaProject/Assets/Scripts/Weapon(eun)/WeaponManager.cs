using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;


namespace WeaponEun
{

    // 무기 유형을 정의하는 열거형
    public enum WeaponType
    {
        // 임시 무기 이름
        ElectroGun,
        FlameCannon,
        AquaCannon
    }

    public class WeaponManager : MonoBehaviour
    {
        // 현재 장착된 무기
        public WeaponType currentWeapon;

        // 총알 프리팹
        public GameObject[] E_Bullet, F_Bullet, A_Bullet;


        // 스킬 프리팹
        public GameObject E_Skill, F_Skill, A_Skill;

        // 총알 발사 위치 (Player의 발사 위치)
        public Transform firePoint1, firePoint2, firePoint3;

        public int power = 0;
        private GameObject powerup;
        
        // Rocket 관련 변수
        private bool isRocket = false;
        private float rocketTime = 4f; // 로켓 발사 지속 시간 (초)
        private float rocketCool = 0.3f; // 발사 간격 (초)
        private float rocketTimer = 0f; // 발사 타이머

        // Missile 관련 변수
        private bool isMissile = false; // Missile 스킬 활성화 여부
        private float missileTime = 4f; // Missile 발사 지속 시간 (초)
        private float missileCool = 0.3f; // Missile 발사 간격 (초)
        private float missileTimer = 0f; // Missile 발사 타이머


        private bool isSkillOnCooldown = false; // 스킬 쿨타임 여부
        private float skillCooldown = 10f; // 스킬 쿨타임
        private float skillCooldownTimer = 0f; // 스킬 쿨타임 타이머



        // 업그레이드 메소드
        public void UpgradeWeapon()
        {
            power += 1;
            if (power > 2)
                power = 2;

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
                case WeaponType.ElectroGun:
                    // 첫 번째 총알 발사
                    GameObject eBullet1 = Instantiate(E_Bullet[power], firePoint1.position, firePoint1.rotation);
                    bulletScript1 = eBullet1.GetComponent<PBullet>();
                    if (bulletScript1 != null)
                    {
                        bulletScript1.isHoming = true; // ElectroGun만 타겟팅 활성화
                    }

                    // 두 번째 총알 발사
                    GameObject eBullet2 = Instantiate(E_Bullet[power], firePoint2.position, firePoint2.rotation);
                    bulletScript2 = eBullet2.GetComponent<PBullet>();
                    if (bulletScript2 != null)
                    {
                        bulletScript2.isHoming = true; // ElectroGun만 타겟팅 활성화
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

            isSkillOnCooldown = true; // 스킬 쿨타임 시작
            skillCooldownTimer = skillCooldown; // 쿨타임 타이머 설정

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
                    isMissile = true; // Missile 스킬 활성화
                    missileTime = 4f; // 발사 지속 시간 설정
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
                    currentWeapon = WeaponType.ElectroGun;
                    Debug.Log("ElectroGun 장착됨");
                    break;
                case 2:
                    currentWeapon = WeaponType.FlameCannon;
                    Debug.Log("FlameCannon 장착됨");
                    break;
                case 3:
                    currentWeapon = WeaponType.AquaCannon;
                    Debug.Log("AquaCannon 장착됨");
                    break;
                default:
                    Debug.LogError("잘못된 무기 인덱스: " + weaponIndex);
                    break;
            }
        }


        void Start()
        {

        }

        void Update()
        {
            // B키로 스킬 사용 (WeaponManager 안에서 처리)
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
                skillCooldownTimer -= Time.deltaTime;
                if (skillCooldownTimer <= 0f)
                {
                    isSkillOnCooldown = false;
                }
            }


            // 로켓 스킬 처리
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

            // Missile 스킬 처리
            if (isMissile)
            {
                missileTimer -= Time.deltaTime;

                if (missileTimer <= 0f)
                {
                    Instantiate(A_Skill, firePoint3.position, firePoint3.rotation); // Missile 발사
                    missileTimer = missileCool; // 타이머 리셋
                }

                missileTime -= Time.deltaTime;
                if (missileTime <= 0f)
                {
                    isMissile = false; // Missile 스킬 종료
                }
            }

        }

    }
}