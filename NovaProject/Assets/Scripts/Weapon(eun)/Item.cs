using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player와 충돌! 아이템 효과 발동!");
            WeaponManager weaponManager = collision.GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.UpgradeWeapon(); // 업그레이드 메서드 호출
            }
            else
            {
                Debug.LogError("WeaponManager를 찾을 수 없습니다. 플레이어에 WeaponManager가 붙어있는지 확인하세요.");
            }

            Destroy(gameObject); // 아이템 제거
        }
        else
        {
            Debug.Log("충돌했지만 Player가 아님: " + collision.name);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

}


