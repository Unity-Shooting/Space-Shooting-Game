using System.Collections;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// °ÔÀÓÀ» ÃÑ°ýÇÏ´Â GameManager Å¬·¡½º.
/// </summary>
public class GameManager : Singleton<GameManager>
{

    /*
     ???? ì½ë ì¶ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶ê?? ë°? 2ì´? ì¶ë ¥
     */

    /// <summary>
    /// GameStart UI ?´ë¯¸ì?? ( Fade In / Fade Out ?¬?©? ??´ CanvasGroup ?¬?©)
    /// </summary>
    public CanvasGroup gameStartCanvasGroup;

    /// <summary>
    /// GameOver UI ?´ë¯¸ì?? ( Fade In / Fade Out ?¬?©? ??´ CanvasGroup ?¬?©)
    /// </summary>
    public CanvasGroup gameOverCanvasGroup;

    /// <summary>
    /// GameOver ??¤?¸ (?´ë¦? ? ë©ì¸ ë©ë´ ?´?, ê¹ë¹¡?´? ?¨ê³? ì¶ê??)
    /// </summary>
    public CanvasGroup gameOverTextCanvasGroup;

    private bool isFadingText = true; // GameOver ??¤?¸ ê¹ë¹¡?´ê¸? ?¬ë¶?

    /// <summary>
    /// ??´? ??
    /// </summary>
    public float fadeDuration = 0.5f;
    // GameOver ??¤?¸ ??´? ??
    public float gameOverTextFadeDuration = 1f;

    public bool CanClick = false;

    /*******************?? *********************/

    /// <summary>
    /// GameManagerÀÇ ·Î±× ÅÂ±×.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// µð¹ö±ë ·Î±× Ãâ·Â ON/OFF
    /// </summary>
    public bool logOn = true;

    /// <summary>
    /// ÃÊ±âÈ­ ÇÔ¼ö.
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

        /*
        ???? ì½ë ì¶ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶ê?? ë°? 2ì´? ì¶ë ¥
        */
        //BGMManager.Instance.PlayBGM1(); -> StartGame()?¼ë¡? ?®ê¹?

        StartCoroutine(ShowGameStart());

        /*******************?? *********************/

    }




    /// <summary>
    /// ê²ì ??
    /// </summary>
        /*
        ???? ì½ë ì¶ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶ê?? ë°? 2ì´? ì¶ë ¥
        */
    private void StartGame()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] Game Start!");
        BGMManager.Instance.PlayBGM1();
    }


    /// <summary>
    /// GameStart ?ë©´ì ??´? ?¸/???ë©? ë³´ì¬ì£¼ê³  2ì´? ? ê²ì ??
    /// </summary>
    /// 
    IEnumerator ShowGameStart()
    {
        if (gameStartCanvasGroup != null)
        {
            // ê¸°ì¡´ gameStartImage ??±? ì½ë ???? , ??´? ?¸ ?¨ê³? ? ?©
            //ì½ë£¨?´ ?? FadeCanvasGroup(?¬?©?  ìº í¼?¤ê·¸ë£¹, ?? ?? ê°? 0 ?´ë©? ?¬ëª?  1?´ë©? ë¶í¬ëª?, ??¼ , ??´? ì§?? ?ê°ì¸?° ?? ? ??´?? 1f, true?´ë©? Time.timeScale = 0 ?´?¬? ??? ?¨. ? ?´?ì§? ??¼ë©? ê¸°ë³¸ê°? False)

            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 0f, 1f)); // ??? ë°ìì§?
            yield return new WaitForSeconds(2f);        //2ì´? ?¤
            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 1f, 0f)); // ??? ?¬?¼ì§?
        }

        StartGame();
    }


    public void GameOver()
    {
        /*Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        BGMManager.Instance.PlayBGM2();
        -> ShowGameOverScreen() ?? ì§ì´?£?
         */
        PWRManager.Instance.Run = false;
        Debug.Log("Run : "+ PWRManager.Instance.Run);
        StartCoroutine(ShowGameOverScreen());   //ì½ë£¨?´ ?¤?
    }


    /// <summary>
    /// Game Over ?ë©´ì ??°ê³? ê²ì ? ì§?
    /// </summary>
    IEnumerator ShowGameOverScreen()
    {


        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        CanClick = true;
        // BGM ë³?ê²?
        BGMManager.Instance.PlayBGM2();

        // ê¸°ì¡´ gameOverImage ??±? ì½ë ???? , ??´? ?¸ ?¨ê³? ? ?©
        if (gameOverCanvasGroup != null)
        {
            //ì½ë£¨?´ ?? FadeCanvasGroup(?¬?©?  ìº í¼?¤ê·¸ë£¹, ?? ?? ê°? 0 ?´ë©? ?¬ëª?  1?´ë©? ë¶í¬ëª?, ??¼ , ??´? ì§?? ?ê°ì¸?° ?? ? ??´?? 1f, true?´ë©? Time.timeScale = 0 ?´?¬? ??? ?¨. ? ?´?ì§? ??¼ë©? ê¸°ë³¸ê°? False)
            yield return StartCoroutine(FadeCanvasGroup(gameOverCanvasGroup, 0f, 1f));
        }
        // GameOver ??¤?¸ ê¹ë¹¡?´ê¸? ??
        if (gameOverTextCanvasGroup != null)
        {
            StartCoroutine(FadeLoopGameOverText());
        }

        // ê²ì ë©ì¶ê¸?
        Time.timeScale = 0;

    }

    /// <summary>
    /// ÇÁ·¹ÀÓ¸¶´Ù È£ÃâµÇ´Â ¾÷µ¥ÀÌÆ® ÇÔ¼ö.
    /// </summary>
    void Update()
    {

        /*
         ???? ì½ë ì¶ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶ê?? ë°? 2ì´? ì¶ë ¥
         */
        if (CanClick && isFadingText && Input.GetKeyDown(KeyCode.Z))
        {
            isFadingText = false; // ì½ë£¨?´ ì¢ë£ ??ê·? ?¤? 
            CanClick = false;
            LoadMainMenu();
        }


        // TODO: ?? ?´?´? ì²´ë ¥?´ 0 ?´??´ë©? ê²ì ì¢ë£ ?±? ë¬¸êµ¬ ??°ê¸?
        // TODO: UI ?ë©´ì ???ê¸?
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManagerÀÇ UI º¯°æ ÇÔ¼ö¸¦ ½ÇÇàÇÏ´Â Å×½ºÆ® ÇÔ¼ö.
    /// </summary>
    public void changeUI()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] changeUI");
    }

    /// <summary>
    /// GameOver ??¤?¸ê°? ê³ì ê¹ë¹¡?´?ë¡? ë°ë³µ
    /// </summary>
    /*
    ???? ì½ë ì¶ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶ê?? ë°? 2ì´? ì¶ë ¥
    */
    private IEnumerator FadeLoopGameOverText()
    {
        while (isFadingText)
        {
            //ì½ë£¨?´ ?? FadeCanvasGroup(?¬?©??¹ ìº í¼?¤ê·¸ë£¹, ?? ?? ê°? 0 ?´ë©? ?¬ëª?  1?´ë©? ë¶í¬ëª?, ??¼ , ??´? ì§?? ?ê°ì¸?° ?? ? ??´?? 1f, true?´ë©? Time.timeScale = 0 ?´?¬? ??? ?¨.)
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 0f, 1f, gameOverTextFadeDuration, true));
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 1f, 0f, gameOverTextFadeDuration, true));

        }
    }

    /// <summary>
    ///  ë©ì¸ ë©ë´ë¡? ?´??? ?¨?
    /// </summary>
    /// 
    /*
     ???? ì½ë ì¶ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶ê?? ë°? 2ì´? ì¶ë ¥
     */
    private void LoadMainMenu()
    {
        Time.timeScale = 1; //  ?¬ ë³?ê²? ?  TimeScale? ?¤? 1ë¡? ?¤? 
        DestroyAllPersistentObjects();
        SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// CanvasGroup? ??©? ??´? ?¨ê³? (Unscaled Time ? ?© ê°??¥)
    /// </summary>
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration = -1f, bool useUnscaledTime = false)
    {
        if (duration < 0) duration = fadeDuration;      //duration?´ ê¸°ë³¸ê°?(-1f)?¼ ê²½ì°, ?´??¤? ? ?? fadeDuration ê°ì ?¬?©
        float elapsedTime = 0f;                                             //ê²½ê³¼ ?ê°? ì´ê¸°?

        while (elapsedTime < duration)                            //?¤? ? duration ?? while ë£¨í ?¤? (??´? ?¨ê³? ? ?©)
        {
            elapsedTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;               //useUnscaledTime?´ trueë©? Time.unscaledDeltaTime ?¬?© (ê²ì ? ì§? ????? ??) , falseë©? ?¼ë°? Time.deltaTime ?¬?© (ê²ì ì§í ì¤ì¼ ?ë§? ??)
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);       //Lerp ?¨?ë¥? ?¬?©??¬ alpha ê°ì startAlpha ? endAlphaë¡? ? ì§ì ?¼ë¡? ë³?ê²? (ë³´ì???¤ê°? ?ë³´ì???¤ê°? ? ?±)
            yield return null;                                                                                                                                                  // ?¤? ?? ?ê¹ì?? ???ê¸?
        }

        canvasGroup.alpha = endAlpha;
    }


    /// <summary>
    /// MainMenu Secen ?¼ë¡? ??´ê°ë, DontDestroyOnLoad object ? ê±?
    /// </summary>
    private void DestroyAllPersistentObjects()
{
     GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);     //?¬? ?? ëª¨ë  ?¤ë¸ì ?¸ë¥? ê²??

        foreach (GameObject obj in allObjects)  //ëª¨ë  ?¤ë¸ì ?¸ë¥? foreachë¥? ?¬?©??¬ ???ë©? ì²ë¦¬
        {
            //obj.scene.handle == -1ë¡? ?´?¹ ?¤ë¸ì ?¸ê°? DontDestroyOnLoad ?¬? ???ì§? ??¸
            if (obj.scene.buildIndex == -1) // DontDestroyOnLoad? ?? ?¤ë¸ì ?¸?¸ì§? ??¸
        {
                //?´?¹ ?¤ë¸ì ?¸ë¥? ?­? 
                Destroy(obj);
        }
    }
}

    /*******************?? *********************/

}
