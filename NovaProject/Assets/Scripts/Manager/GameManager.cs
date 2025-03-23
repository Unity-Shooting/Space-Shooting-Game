using UnityEngine;

/// <summary>
/// 게임을 총괄하는 GameManager 클래스.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// GameManager의 로그 태그.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// 디버깅 로그 출력 ON/OFF
    /// </summary>
    public bool logOn = true;

    /// <summary>
    /// 초기화 함수.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 프레임마다 호출되는 업데이트 함수.
    /// </summary>
    void Update()
    {
        // TODO: 플레이어의 체력이 0 이하이면 게임 종료 등의 문구 띄우기
        // TODO: UI 화면에 표시하기
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManager의 UI 변경 함수를 실행하는 테스트 함수.
    /// </summary>
    public void changeUI()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] changeUI");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
    }
}
