using System.Collections;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// °ÔÀÓÀ» ÃÑ°ıÇÏ´Â GameManager Å¬·¡½º.
/// </summary>
public class GameManager : Singleton<GameManager>
{

    /*
     ????˜„ ì½”ë“œ ì¶”ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶”ê?? ë°? 2ì´? ì¶œë ¥
     */

    /// <summary>
    /// GameStart UI ?´ë¯¸ì?? ( Fade In / Fade Out ?‚¬?š©?„ ?œ„?•´ CanvasGroup ?‚¬?š©)
    /// </summary>
    public CanvasGroup gameStartCanvasGroup;

    /// <summary>
    /// GameOver UI ?´ë¯¸ì?? ( Fade In / Fade Out ?‚¬?š©?„ ?œ„?•´ CanvasGroup ?‚¬?š©)
    /// </summary>
    public CanvasGroup gameOverCanvasGroup;

    /// <summary>
    /// GameOver ?…?Š¤?Š¸ (?´ë¦? ?‹œ ë©”ì¸ ë©”ë‰´ ?´?™, ê¹œë¹¡?´?Š” ?š¨ê³? ì¶”ê??)
    /// </summary>
    public CanvasGroup gameOverTextCanvasGroup;

    private bool isFadingText = true; // GameOver ?…?Š¤?Š¸ ê¹œë¹¡?´ê¸? ?—¬ë¶?

    /// <summary>
    /// ?˜?´?“œ ?†?„
    /// </summary>
    public float fadeDuration = 0.5f;
    // GameOver ?…?Š¤?Š¸ ?˜?´?“œ ?†?„
    public float gameOverTextFadeDuration = 1f;

    public bool CanClick = false;

    /*******************?ˆ˜? •*********************/

    /// <summary>
    /// GameManagerÀÇ ·Î±× ÅÂ±×.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// µğ¹ö±ë ·Î±× Ãâ·Â ON/OFF
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
        ????˜„ ì½”ë“œ ì¶”ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶”ê?? ë°? 2ì´? ì¶œë ¥
        */
        //BGMManager.Instance.PlayBGM1(); -> StartGame()?œ¼ë¡? ?˜®ê¹?

        StartCoroutine(ShowGameStart());

        /*******************?ˆ˜? •*********************/

    }




    /// <summary>
    /// ê²Œì„ ?‹œ?‘
    /// </summary>
        /*
        ????˜„ ì½”ë“œ ì¶”ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶”ê?? ë°? 2ì´? ì¶œë ¥
        */
    private void StartGame()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] Game Start!");
        BGMManager.Instance.PlayBGM1();
    }


    /// <summary>
    /// GameStart ?™”ë©´ì„ ?˜?´?“œ ?¸/?•„?›ƒ?•˜ë©? ë³´ì—¬ì£¼ê³  2ì´? ?›„ ê²Œì„ ?‹œ?‘
    /// </summary>
    /// 
    IEnumerator ShowGameStart()
    {
        if (gameStartCanvasGroup != null)
        {
            // ê¸°ì¡´ gameStartImage ?™œ?„±?™” ì½”ë“œ ????‹ , ?˜?´?“œ ?¸ ?š¨ê³? ? ?š©
            //ì½”ë£¨?‹´ ?‹œ?‘ FadeCanvasGroup(?‚¬?š©?•  ìº í¼?Š¤ê·¸ë£¹, ?‹œ?‘ ?•Œ?ŒŒ ê°? 0 ?´ë©? ?ˆ¬ëª?  1?´ë©? ë¶ˆíˆ¬ëª?, ?™?¼ , ?˜?´?“œ ì§??† ?‹œê°„ì¸?° ?œ„?— ? •?˜?•´?†“?Œ 1f, true?´ë©? Time.timeScale = 0 ?´?—¬?„ ?™?‘?„ ?•¨. ? ?–´?†“ì§? ?•Š?œ¼ë©? ê¸°ë³¸ê°? False)

            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 0f, 1f)); // ?„œ?„œ?ˆ ë°ì•„ì§?
            yield return new WaitForSeconds(2f);        //2ì´? ?’¤
            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 1f, 0f)); // ?„œ?„œ?ˆ ?‚¬?¼ì§?
        }

        StartGame();
    }


    public void GameOver()
    {
        /*Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        BGMManager.Instance.PlayBGM2();
        -> ShowGameOverScreen() ?•ˆ?— ì§‘ì–´?„£?Œ
         */
        PWRManager.Instance.Run = false;
        Debug.Log("Run : "+ PWRManager.Instance.Run);
        StartCoroutine(ShowGameOverScreen());   //ì½”ë£¨?‹´ ?‹¤?–‰
    }


    /// <summary>
    /// Game Over ?™”ë©´ì„ ?„?š°ê³? ê²Œì„ ? •ì§?
    /// </summary>
    IEnumerator ShowGameOverScreen()
    {


        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        CanClick = true;
        // BGM ë³?ê²?
        BGMManager.Instance.PlayBGM2();

        // ê¸°ì¡´ gameOverImage ?™œ?„±?™” ì½”ë“œ ????‹ , ?˜?´?“œ ?¸ ?š¨ê³? ? ?š©
        if (gameOverCanvasGroup != null)
        {
            //ì½”ë£¨?‹´ ?‹œ?‘ FadeCanvasGroup(?‚¬?š©?•  ìº í¼?Š¤ê·¸ë£¹, ?‹œ?‘ ?•Œ?ŒŒ ê°? 0 ?´ë©? ?ˆ¬ëª?  1?´ë©? ë¶ˆíˆ¬ëª?, ?™?¼ , ?˜?´?“œ ì§??† ?‹œê°„ì¸?° ?œ„?— ? •?˜?•´?†“?Œ 1f, true?´ë©? Time.timeScale = 0 ?´?—¬?„ ?™?‘?„ ?•¨. ? ?–´?†“ì§? ?•Š?œ¼ë©? ê¸°ë³¸ê°? False)
            yield return StartCoroutine(FadeCanvasGroup(gameOverCanvasGroup, 0f, 1f));
        }
        // GameOver ?…?Š¤?Š¸ ê¹œë¹¡?´ê¸? ?‹œ?‘
        if (gameOverTextCanvasGroup != null)
        {
            StartCoroutine(FadeLoopGameOverText());
        }

        // ê²Œì„ ë©ˆì¶”ê¸?
        Time.timeScale = 0;

    }

    /// <summary>
    /// ÇÁ·¹ÀÓ¸¶´Ù È£ÃâµÇ´Â ¾÷µ¥ÀÌÆ® ÇÔ¼ö.
    /// </summary>
    void Update()
    {

        /*
         ????˜„ ì½”ë“œ ì¶”ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶”ê?? ë°? 2ì´? ì¶œë ¥
         */
        if (CanClick && isFadingText && Input.GetKeyDown(KeyCode.Z))
        {
            isFadingText = false; // ì½”ë£¨?‹´ ì¢…ë£Œ ?”Œ?˜ê·? ?„¤? •
            CanClick = false;
            LoadMainMenu();
        }


        // TODO: ?”Œ? ˆ?´?–´?˜ ì²´ë ¥?´ 0 ?´?•˜?´ë©? ê²Œì„ ì¢…ë£Œ ?“±?˜ ë¬¸êµ¬ ?„?š°ê¸?
        // TODO: UI ?™”ë©´ì— ?‘œ?‹œ?•˜ê¸?
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
    /// GameOver ?…?Š¤?Š¸ê°? ê³„ì† ê¹œë¹¡?´?„ë¡? ë°˜ë³µ
    /// </summary>
    /*
    ????˜„ ì½”ë“œ ì¶”ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶”ê?? ë°? 2ì´? ì¶œë ¥
    */
    private IEnumerator FadeLoopGameOverText()
    {
        while (isFadingText)
        {
            //ì½”ë£¨?‹´ ?‹œ?‘ FadeCanvasGroup(?‚¬?š©?•˜?„¹ ìº í¼?Š¤ê·¸ë£¹, ?‹œ?‘ ?•Œ?ŒŒ ê°? 0 ?´ë©? ?ˆ¬ëª?  1?´ë©? ë¶ˆíˆ¬ëª?, ?™?¼ , ?˜?´?“œ ì§??† ?‹œê°„ì¸?° ?œ„?— ? •?˜?•´?†“?Œ 1f, true?´ë©? Time.timeScale = 0 ?´?—¬?„ ?™?‘?„ ?•¨.)
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 0f, 1f, gameOverTextFadeDuration, true));
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 1f, 0f, gameOverTextFadeDuration, true));

        }
    }

    /// <summary>
    ///  ë©”ì¸ ë©”ë‰´ë¡? ?´?™?•˜?Š” ?•¨?ˆ˜
    /// </summary>
    /// 
    /*
     ????˜„ ì½”ë“œ ì¶”ê?? : 2025 - 03 - 26 GameStart, GameOver ?´ë¯¸ì?? ì¶”ê?? ë°? 2ì´? ì¶œë ¥
     */
    private void LoadMainMenu()
    {
        Time.timeScale = 1; //  ?”¬ ë³?ê²? ? „ TimeScale?„ ?‹¤?‹œ 1ë¡? ?„¤? •
        DestroyAllPersistentObjects();
        SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// CanvasGroup?„ ?™œ?š©?•œ ?˜?´?“œ ?š¨ê³? (Unscaled Time ? ?š© ê°??Š¥)
    /// </summary>
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration = -1f, bool useUnscaledTime = false)
    {
        if (duration < 0) duration = fadeDuration;      //duration?´ ê¸°ë³¸ê°?(-1f)?¼ ê²½ìš°, ?´?˜?Š¤?— ? •?˜?œ fadeDuration ê°’ì„ ?‚¬?š©
        float elapsedTime = 0f;                                             //ê²½ê³¼ ?‹œê°? ì´ˆê¸°?™”

        while (elapsedTime < duration)                            //?„¤? •?•œ duration ?™?•ˆ while ë£¨í”„ ?‹¤?–‰ (?˜?´?“œ ?š¨ê³? ? ?š©)
        {
            elapsedTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;               //useUnscaledTime?´ trueë©? Time.unscaledDeltaTime ?‚¬?š© (ê²Œì„ ? •ì§? ?ƒ?ƒœ?—?„œ?„ ?™?‘) , falseë©? ?¼ë°? Time.deltaTime ?‚¬?š© (ê²Œì„ ì§„í–‰ ì¤‘ì¼ ?•Œë§? ?™?‘)
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);       //Lerp ?•¨?ˆ˜ë¥? ?‚¬?š©?•˜?—¬ alpha ê°’ì„ startAlpha ?†’ endAlphaë¡? ? ì§„ì ?œ¼ë¡? ë³?ê²? (ë³´ì???‹¤ê°? ?•ˆë³´ì???‹¤ê°? ? ?“±)
            yield return null;                                                                                                                                                  // ?‹¤?Œ ?”„? ˆ?„ê¹Œì?? ???ê¸?
        }

        canvasGroup.alpha = endAlpha;
    }


    /// <summary>
    /// MainMenu Secen ?œ¼ë¡? ?„˜?–´ê°ˆë•Œ, DontDestroyOnLoad object ? œê±?
    /// </summary>
    private void DestroyAllPersistentObjects()
{
     GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);     //?”¬?— ?ˆ?Š” ëª¨ë“  ?˜¤ë¸Œì ?Š¸ë¥? ê²??ƒ‰

        foreach (GameObject obj in allObjects)  //ëª¨ë“  ?˜¤ë¸Œì ?Š¸ë¥? foreachë¥? ?‚¬?š©?•˜?—¬ ?ˆœ?šŒ?•˜ë©? ì²˜ë¦¬
        {
            //obj.scene.handle == -1ë¡? ?•´?‹¹ ?˜¤ë¸Œì ?Š¸ê°? DontDestroyOnLoad ?”¬?— ?†?•˜?Š”ì§? ?™•?¸
            if (obj.scene.buildIndex == -1) // DontDestroyOnLoad?— ?ˆ?Š” ?˜¤ë¸Œì ?Š¸?¸ì§? ?™•?¸
        {
                //?•´?‹¹ ?˜¤ë¸Œì ?Š¸ë¥? ?‚­? œ
                Destroy(obj);
        }
    }
}

    /*******************?ˆ˜? •*********************/

}
