using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
public class PlayerController : Singleton<PlayerController>
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
    /// 실드 지속 시간
    /// </summary>
    public float shieldDuration = 3f;

    /// <summary>
    /// 실드 남은 쿨타임
    /// </summary>
    public float shieldCoolTime = 10f;

    /// <summary>
    /// 실드 가능
    /// </summary>
    private bool isShieldReady = true;

    /// <summary>
    /// 마우스 방향대로 플레이어 방향을 바라보도록 설정하는 플래그.
    /// </summary>
    public bool useMouseDirection = false;

    /// <summary>
    /// 기본 게임 오브젝트 (플레이어 이동 시 사용됨).
    /// </summary>
    public GameObject Base;

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
    /// 오토캐논 애니메이터.
    /// </summary>
    public Animator amAutoCannon;

    /// <summary>
    /// 빅스페이스건 애니메이터.
    /// </summary>
    public Animator amBigSpaceGun;

    /// <summary>
    /// 전면 방어막 애니메이터 (방어 기능을 담당할 것으로 예상됨).
    /// </summary>
    public Animator FrontSideShield;

    /// <summary>
    /// 방어막 애니메이터
    /// </summary>
    public GameObject RoundShield;

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
    /// Main Camera의 크기를 가져와서 Player의 반경을 제한.
    /// </summary>
    private Camera cam;

    /// <summary>
    /// 초기화 작업을 수행하는 Awake 메서드.
    /// Rigidbody2D 및 입력 시스템을 초기화한다.
    /// </summary>
    protected override void Awake()
    {
        inputSystem = new MyInputSystemActions(); // 입력 시스템 초기화
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        cam = Camera.main; // main 카메라 저장.

        //if(GameManager.Instance.logging) Debug.Log($"[{TAG}] Awake before base.Awake");
        base.Awake(); // 나중에 실행해야 Null에러 안남
        //if(GameManager.Instance.logging) Debug.Log($"[{TAG}] Awake after base.Awake");
    }

    /// <summary>
    /// 객체가 활성화될 때 호출되며, 입력 시스템을 활성화하고 공격 이벤트를 바인딩한다.
    /// </summary>
    private void OnEnable()
    {
        inputSystem.Enable(); // 입력 시스템 활성화
        inputSystem.Player.Space.performed += _ => Attack(); // 공격 입력 이벤트 등록
        inputSystem.Player.LeftClick.performed += _ => Click(); // 클릭 입력 이벤트 등록 (마우스 클릭도 공격 처리)
        inputSystem.Player.Shift.performed += _ => Shield(); // 실드 이벤트 등록
        inputSystem.Player.X.performed += _ => XBtn(); // X 버튼 이벤트 등록
    }

    /// <summary>
    /// 객체가 비활성화될 때 호출되며, 공격 이벤트를 해제한다.
    /// </summary>
    private void OnDisable()
    {
        inputSystem.Player.Space.performed -= _ => Attack(); // 공격 입력 이벤트 해제
        inputSystem.Player.LeftClick.performed -= _ => Click(); // 클릭 입력 이벤트 해제
        inputSystem.Player.Shift.performed -= _ => Shield(); // 실드 이벤트 해제
        inputSystem.Player.X.performed -= _ => XBtn(); // X 버튼 이벤트 해제
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
        ClampToCameraView(); // Rigidbody2D의 범위를 Cam 범위 안으로 제한.
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
        //if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] Attack"); // 공격 발생 로그 출력
        //weaponCotroller.Shooting(); // 무기 발사          2025 - 03 -23 
        // am.SetTrigger("shoot");         // 2025 - 03 -23  추가
        //SFXManager.Instance.ShootSound();       // 2025 - 03 -23  추가

        amAutoCannon.SetTrigger("shoot");
    }

    /// <summary>
    /// 마우스 왼쪽 버튼 클릭 시 이벤트
    /// </summary>
    private void Click()
    {
    }

    /// <summary>
    /// X 버튼 클릭 시 이벤트
    /// </summary>
    private void XBtn()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (GameManager.Instance.logOn) Debug.Log($"{TAG} {scene.name}");
        if(scene.name == "StageOne") SceneManager.LoadScene("StageTwo");
        if(scene.name == "StageTwo") SceneManager.LoadScene("StageOne");
    }

    /// <summary>
    /// 플레이어가 실드를 활성화하는 메서드
    /// </summary>
    private void Shield()
    {
        if (isShieldReady) // 실드가 사용 가능한 상태인지 확인
        {
            isShieldReady = false; // 실드 사용 가능 상태를 비활성화
            PlayerHealth.Instance.isShieldOn = true; // 플레이어의 체력 시스템에서 실드 활성화
            RoundShield.SetActive(true); // 실드 오브젝트 활성화
            StartCoroutine(CloseShield()); // 실드를 일정 시간이 지난 후 비활성화하는 코루틴 시작
        }
        else
        {
            if (GameManager.Instance.logOn) Debug.Log($"실드 쿨타임 : {shieldCoolTime}");
        }
    }

    /// <summary>
    /// 일정 시간이 지나면 실드를 비활성화하고, 쿨타임을 초기화하는 코루틴
    /// </summary>
    IEnumerator CloseShield()
    {
        float maxShieldCoolTime = shieldCoolTime; // 실드의 최대 쿨타임을 저장
        for (int i = 0; i < maxShieldCoolTime; i++) // 실드 지속 시간 동안 반복
        {
            yield return new WaitForSeconds(1f); // 1초 대기
            shieldCoolTime--; // 남은 쿨타임 감소
            if (i == shieldDuration - 1) // 실드 지속 시간이 끝나면 실드 비활성화
            {
                PlayerHealth.Instance.isShieldOn = false;
                RoundShield.SetActive(false);
            }
            if (i == maxShieldCoolTime - 1) // 쿨타임이 끝나면 실드를 다시 사용할 수 있도록 설정
            {
                isShieldReady = true;
                shieldCoolTime = maxShieldCoolTime;
            }
        }
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

    /// <summary>
    /// Rigidbody2D의 위치가 카메라를 넘어가지 않도록 제한한다.
    /// </summary>
    void ClampToCameraView()
    {
        Vector3 camPos = cam.transform.position; // 카메라 뷰의 중앙 좌표
        float height = cam.orthographicSize; // 카메라의 Y 크기의 절반
        float width = height * cam.aspect; // 카메라의 X 크기의 절반

        // 화면의 경계 계산 (카메라 중심 + 화면 크기)
        float minX = camPos.x - width;  // 최소 X값 = 카메라 중앙 - 카메라 X 크기의 절반
        float maxX = camPos.x + width;  // 최대 X값 = 카메라 중앙 + 카메라 X 크기의 절반
        float minY = camPos.y - height; // 최소 Y값 = 카메라 중앙 - 카메라 Y 크기의 절반
        float maxY = camPos.y + height; // 최대 Y값 = 카메라 중앙 + 카메라 Y 크기의 절반

        // 우주선 위치 제한
        Vector2 clampedPos = new Vector2(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );

        rb.position = clampedPos;
    }

}
