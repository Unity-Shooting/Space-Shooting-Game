using UnityEngine;
using UnityEngine.UIElements;


namespace WeaponEun {

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
        public GameObject E_Skill;
        public GameObject F_Skill;
        public GameObject A_Skill;

        // 총알 발사 위치 (Player의 발사 위치)
        public Transform firePoint;

        public int power = 0;
        private GameObject powerup;


        // 업그레이드 메소드
        public void UpgradeWeapon()
        {
            power += 1;
            if (power <= 1)
                power = 1;

            //파워업 메세지 출력
            /*GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
            Destroy(go, 1);*/
        }

        // 총알 발사 메소드
        public void Fire()
        {

            PBullet bulletScript = null; // PBullet 참조 변수 초기화
            switch (currentWeapon)
            {
                case WeaponType.ElectroGun:
                    GameObject eBullet = Instantiate(E_Bullet[power], firePoint.position, firePoint.rotation);
                    bulletScript = eBullet.GetComponent<PBullet>();

                    // PBullet 스크립트를 찾고 isHoming 설정
                    if (bulletScript != null)
                    {
                        bulletScript.isHoming = true; // ElectroGun만 타겟팅 활성화
                    }
                    break;

                case WeaponType.FlameCannon:
                    Instantiate(F_Bullet[power], firePoint.position, firePoint.rotation);
                    break;

                case WeaponType.AquaCannon:
                    Instantiate(A_Bullet[power], firePoint.position, firePoint.rotation);
                    break;

                default:
                    Debug.LogError("알 수 없는 무기 유형입니다.");
                    return;
            }
        }

        // 스킬 사용 메소드
        public void UseSkill()
        {
            // 현재 장착된 무기 유형에 따라 스킬을 발사
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


        // 스킬 발사 메소드
        private void FireSkill(GameObject skillPrefab)
        {
            // 스킬 생성
            Instantiate(skillPrefab, firePoint.position, firePoint.rotation);
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
        }

    }
}
