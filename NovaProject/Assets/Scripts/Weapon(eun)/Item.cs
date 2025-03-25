using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player�� �浹! ������ ȿ�� �ߵ�!");
            WeaponEun.WeaponManager weaponManager = collision.GetComponent<WeaponEun.WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.UpgradeWeapon(); // ���׷��̵� �޼��� ȣ��
            }
            else
            {
                Debug.LogError("WeaponManager�� ã�� �� �����ϴ�. �÷��̾ WeaponManager�� �پ��ִ��� Ȯ���ϼ���.");
            }

            Destroy(gameObject); // ������ ����
        }
        else
        {
            Debug.Log("�浹������ Player�� �ƴ�: " + collision.name);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

}


