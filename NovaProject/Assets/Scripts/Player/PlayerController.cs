using UnityEngine;

/// <summary>
/// 무기 유형을 정의하는 열거형.
/// </summary>
enum WeaponType
{
    AutoCannon,  // 자동 대포
    BigSpaceGun  // 대형 우주포
}

/// <summary>
/// 플레이어의 이동 및 공격을 관리하는 컨트롤러 클래스.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 디버깅을 위한 태그 문자열.
    /// </summary>
    private const string TAG = "PlayerController";

    /// <summary>
    /// 플레이어 이동 속도.
    /// </summary>
    public float speed = 5f;

    /// <summary>
    /// 기본 엔진 게임 오브젝트.
    /// </summary>
    public GameObject BaseEngine;

    /// <summary>
    /// 대형 펄스 엔진 게임 오브젝트.
    /// </summary>
    public GameObject BigPulseEngine;

    /// <summary>
    /// 기본 엔진 효과 애니메이터.
    /// </summary>
    public Animator amBaseEngineEffects;

    /// <summary>
    /// 대형 펄스 엔진 효과 애니메이터.
    /// </summary>
    public Animator amBigPulseEngineEffects;

    /// <summary>
    /// 전면 방어막 애니메이터.
    /// </summary>
    public Animator FrontSideShield;

    /// <summary>
    /// 무기 컨트롤러 참조.
    /// </summary>
    public WeaponController weaponCotroller;

    /// <summary>
    /// 입력된 이동 방향 벡터.
    /// </summary>
    private Vector2 moveInput;

    /// <summary>
    /// 사용자 입력을 관리하는 커스텀 입력 시스템 객체.
    /// </summary>
    private MyInputSystemActions inputSystem;

    /// <summary>
    /// Rigidbody2D 컴포넌트 참조.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// 초기화 작업을 수행하는 Awake 메서드.
    /// Rigidbody2D 및 입력 시스템을 초기화한다.
    /// </summary>
    private void Awake()
    {
        inputSystem = new MyInputSystemActions(); // 입력 시스템 초기화
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
    }

    /// <summary>
    /// 객체가 활성화될 때 호출되며, 입력 시스템을 활성화하고 공격 이벤트를 바인딩한다.
    /// </summary>
    private void OnEnable()
    {
        inputSystem.Enable(); // 입력 시스템 활성화
        inputSystem.Player.Attack.performed += _ => Attack(); // 공격 입력 이벤트 등록
    }

    /// <summary>
    /// 객체가 비활성화될 때 호출되며, 공격 이벤트를 해제한다.
    /// </summary>
    private void OnDisable()
    {
        inputSystem.Player.Attack.performed -= _ => Attack(); // 공격 입력 이벤트 해제
        inputSystem.Disable(); // 입력 시스템 비활성화
    }

    /// <summary>
    /// 플레이어의 공격을 실행하는 메서드.
    /// </summary>
    private void Attack()
    {
        Debug.Log($"{TAG} : Attack"); // 공격 발생 로그 출력
        weaponCotroller.Shooting(); // 무기 발사
    }

    /// <summary>
    /// Unity의 기본 Start 메서드 (현재는 사용되지 않음).
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// 매 프레임마다 호출되며, 플레이어의 입력을 갱신한다.
    /// </summary>
    private void Update()
    {
        move();
    }

    /// <summary>
    /// 플레이어의 이동을 처리하는 메서드.
    /// 입력에 따라 이동 방향을 결정하고 엔진 효과를 조절한다.
    /// </summary>
    private void move()
    {
        moveInput = inputSystem.Player.Move.ReadValue<Vector2>(); // 이동 입력값 업데이트
        //Debug.Log(moveInput.y);

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            amBaseEngineEffects.SetBool("power", true);
        }
        else
        {
            amBaseEngineEffects.SetBool("power", false);
        }
    }

    /// <summary>
    /// 일정한 간격으로 호출되며, Rigidbody2D를 이용해 이동을 처리한다.
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * moveInput); // 이동 처리
    }
}
