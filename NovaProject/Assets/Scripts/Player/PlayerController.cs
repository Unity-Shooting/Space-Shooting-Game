using UnityEngine;
using UnityEngine.InputSystem;

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
    /// 마우스 방향대로 플레이어 방향을 바라보도록 설정하는 플래그.
    /// </summary>
    public bool useMouseDirection = false;

    /// <summary>
    /// 기본 엔진 게임 오브젝트 (플레이어 이동 시 사용됨).
    /// </summary>
    public GameObject BaseEngine;

    /// <summary>
    /// 대형 펄스 엔진 게임 오브젝트 (추진력 강화 시 사용될 수도 있음).
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
    /// 전면 방어막 애니메이터 (방어 기능을 담당할 것으로 예상됨).
    /// </summary>
    public Animator FrontSideShield;

    /// <summary>
    /// 무기 컨트롤러 참조 (플레이어의 무기 발사를 관리함).
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
    /// Rigidbody2D 컴포넌트 참조 (물리 이동 처리를 위해 필요함).
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
        inputSystem.Player.Click.performed += _ => Attack(); // 클릭 입력 이벤트 등록 (마우스 클릭도 공격 처리)
    }

    /// <summary>
    /// 객체가 비활성화될 때 호출되며, 공격 이벤트를 해제한다.
    /// </summary>
    private void OnDisable()
    {
        inputSystem.Player.Attack.performed -= _ => Attack(); // 공격 입력 이벤트 해제
        inputSystem.Player.Click.performed -= _ => Attack(); // 클릭 입력 이벤트 해제
        inputSystem.Disable(); // 입력 시스템 비활성화
    }

    /// <summary>
    /// 매 프레임마다 호출되며, 플레이어의 방향과 이동을 처리한다.
    /// </summary>
    private void Update()
    {
        direction(); // 플레이어가 바라보는 방향 업데이트
        move(); // 플레이어 이동 처리
    }

    /// <summary>
    /// 일정한 간격으로 호출되며, Rigidbody2D를 이용해 실제 이동을 처리한다.
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * moveInput); // Rigidbody2D 기반 이동 처리
    }

    /// <summary>
    /// 플레이어의 방향을 정하는 메서드.
    /// 마우스의 위치에 따라 바라보는 방향이 달라진다.
    /// </summary>
    private void direction()
    {
        if (useMouseDirection)
        {
            RotateTowardsMouse(transform); // 마우스를 바라보도록 회전
        }
    }

    /// <summary>
    /// 플레이어의 이동을 처리하는 메서드.
    /// 입력값을 받아 이동 방향을 결정하고, 엔진 효과를 활성화한다.
    /// </summary>
    private void move()
    {
        moveInput = inputSystem.Player.Move.ReadValue<Vector2>(); // 이동 입력값 업데이트

        if (moveInput.y > 0)
        {
            amBaseEngineEffects.SetBool("power", true); // 엔진 효과 활성화
        }
        else
        {
            amBaseEngineEffects.SetBool("power", false); // 엔진 효과 비활성화
        }
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
    /// 특정 위치에서 마우스 방향 벡터를 반환하는 함수.
    /// </summary>
    Vector3 GetMouseVectorFromPosition(Vector3 position)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue(); // 마우스 화면 좌표 가져오기
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0)); // 마우스의 월드 좌표 변환
        mouseWorldPos.z = 0; // 2D 환경이므로 z 값을 0으로 설정

        Vector3 mouseDirection = (mouseWorldPos - position).normalized; // 방향 벡터 계산
        return mouseDirection;
    }

    /// <summary>
    /// 오브젝트를 마우스 방향으로 회전시키는 함수.
    /// </summary>
    void RotateTowardsMouse(Transform obj)
    {
        Vector3 direction = GetMouseVectorFromPosition(obj.position); // 현재 위치에서 마우스 방향 벡터 가져오기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 방향 벡터를 각도로 변환 (보정 포함)
        obj.rotation = Quaternion.Euler(0f, 0f, angle); // 회전 적용
    }
}
