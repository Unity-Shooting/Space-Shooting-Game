using UnityEngine;

/// <summary>
/// ê²Œì„?„ ì´ê´„?•˜?Š” GameManager ?´?˜?Š¤.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// GameManager?˜ ë¡œê·¸ ?ƒœê·?.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// ?””ë²„ê¹… ë¡œê·¸ ì¶œë ¥ ON/OFF
    /// </summary>
    public bool logOn = true;

    /// <summary>
    /// ì´ˆê¸°?™” ?•¨?ˆ˜.
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
    /// ?”„? ˆ?„ë§ˆë‹¤ ?˜¸ì¶œë˜?Š” ?—…?°?´?Š¸ ?•¨?ˆ˜.
    /// </summary>
    void Update()
    {
        // TODO: ?”Œ? ˆ?´?–´?˜ ì²´ë ¥?´ 0 ?´?•˜?´ë©? ê²Œì„ ì¢…ë£Œ ?“±?˜ ë¬¸êµ¬ ?„?š°ê¸?
        // TODO: UI ?™”ë©´ì— ?‘œ?‹œ?•˜ê¸?
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManager?˜ UI ë³?ê²? ?•¨?ˆ˜ë¥? ?‹¤?–‰?•˜?Š” ?…Œ?Š¤?Š¸ ?•¨?ˆ˜.
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
