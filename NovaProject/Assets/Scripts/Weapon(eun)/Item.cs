using UnityEngine;

public class Item : MonoBehaviour
{
    public int attackIncrease = 5; // 아이템으로 증가하는 공격력

    /*
     * 대현 작성  : 2025 - 03 - 25
     */
    //아이템 가속 속도
    public float ItemVelocity = 200f; //아이템에 적용할 속도를 나타내는 변수
    Rigidbody2D rig = null; //2D 물리 엔진에서 물체에 물리적 상호작용을 처리하는 컴포넌트를 저장할 변수
    /**/
    public Vector2 backgroundSize = new Vector2 (10.2f, 11.9f); //배경 크기

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
        /*
        * 대현 작성  : 2025 - 03 - 25
        */
        {
            rig = GetComponent<Rigidbody2D>(); //현재 게임 객체에 연결된 Rigidbody2D 컴포넌트를 가져오기
            rig.AddForce(new Vector3(ItemVelocity, ItemVelocity, 0f));  //힘을 추가하는 메서드 ... 움직이게 하는 코드
        }
    }

    void FixedUpdate()
    {
        ItemBounce(); // 아이템 튕기도록 계속 호출
    }

    //250326 낙원에서 추가
    private void ItemBounce()
    {
        Vector2 pos = rig.position; // 아이템 위치
        Vector2 vel = rig.linearVelocity; // 아이템 초기속도

        Vector2 bgCenter = new Vector2(0,0);
        float minX = bgCenter.x - backgroundSize.x / 2; //최소x
        float maxX = bgCenter.x + backgroundSize.x / 2; //최대x
        float minY = bgCenter.y - backgroundSize.y / 2; //최소y
        float maxY = bgCenter.y + backgroundSize.y / 2; //최대y

        // 최대 or 최소값에 닿으면 방향 단위벡터 변경해서 반사
        if ((pos.x < minX && vel.x < 0) || (pos.x > maxX && vel.x > 0))
        {
            vel.x *= -1;
        }
        if ((pos.y < minY && vel.y < 0) || (pos.y > maxY && vel.y > 0))
        {
            vel.y *= -1;
        }

        // 수정된 아이템 경로 반영해서 다시 velocity를 Set
        rig.linearVelocity = vel;
    }

}


