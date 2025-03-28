using UnityEngine;


// 테스트를 위해 임시로 PlayerMove 스크립트 작성했습니다!

public class PlayerMove : MonoBehaviour
{
    // WeaponManager 참조
    public WeaponEun.WeaponManager weaponManager;

    // 이동 속도
    public float speed = 5f;

    // Rigidbody2D 참조
    private Rigidbody2D rb;

    // 이동 입력값
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 초기화
    }



    void Start()
    {
        
    }

    void Update()
    {

        // 이동 입력 업데이트
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        

    }

    private void FixedUpdate()
    {
        // Rigidbody2D를 이용해 이동 처리
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
