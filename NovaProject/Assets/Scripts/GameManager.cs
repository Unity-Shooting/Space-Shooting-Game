using UnityEngine;

/// <summary>
/// 게임을 총괄하는 GameManager 클래스.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// GameManager의 로그 태그.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// 플레이어 컨트롤러 참조.
    /// </summary>
    public PlayerController playerController;

    /// <summary>
    /// 게임 시작 시 실행되는 초기화 함수.
    /// </summary>
    void Start()
    {
        playerController.Init(this);
    }

    /// <summary>
    /// 프레임마다 호출되는 업데이트 함수.
    /// </summary>
    void Update()
    {
        // TODO: 플레이어의 체력이 0 이하이면 게임 종료 등의 문구 띄우기
        // TODO: UI 화면에 표시하기
        Debug.Log(playerController.health);
    }

    /// <summary>
    /// Player에서 GameManager의 UI 변경 함수를 실행하는 테스트 함수.
    /// </summary>
    public void changeUI()
    {
        Debug.Log($"[{TAG}] changeUI");
    }
}
