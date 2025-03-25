using UnityEngine;

/// <summary>
/// κ²μ? μ΄κ΄?? GameManager ?΄??€.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// GameManager? λ‘κ·Έ ?κ·?.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// ?λ²κΉ λ‘κ·Έ μΆλ ₯ ON/OFF
    /// </summary>
    public bool logOn = true;

    /// <summary>
    /// μ΄κΈ°? ?¨?.
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
    /// ?? ?λ§λ€ ?ΈμΆλ? ??°?΄?Έ ?¨?.
    /// </summary>
    void Update()
    {
        // TODO: ?? ?΄?΄? μ²΄λ ₯?΄ 0 ?΄??΄λ©? κ²μ μ’λ£ ?±? λ¬Έκ΅¬ ??°κΈ?
        // TODO: UI ?λ©΄μ ???κΈ?
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManager? UI λ³?κ²? ?¨?λ₯? ?€??? ??€?Έ ?¨?.
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
