using UnityEngine;

/// <summary>
/// 게임?�� 총괄?��?�� GameManager ?��?��?��.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// GameManager?�� 로그 ?���?.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// ?��버깅 로그 출력 ON/OFF
    /// </summary>
    public bool logOn = true;

    /// <summary>
    /// 초기?�� ?��?��.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        BGMManager.Instance.PlayBGM1();
    }

    /// <summary>
    /// ?��?��?��마다 ?��출되?�� ?��?��?��?�� ?��?��.
    /// </summary>
    void Update()
    {
        // TODO: ?��?��?��?��?�� 체력?�� 0 ?��?��?���? 게임 종료 ?��?�� 문구 ?��?���?
        // TODO: UI ?��면에 ?��?��?���?
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManager?�� UI �?�? ?��?���? ?��?��?��?�� ?��?��?�� ?��?��.
    /// </summary>
    public void changeUI()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] changeUI");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        BGMManager.Instance.PlayBGM2();
    }
}
