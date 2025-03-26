using System.Collections;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ������ �Ѱ��ϴ� GameManager Ŭ����.
/// </summary>
public class GameManager : Singleton<GameManager>
{

    /*
     ????�� 코드 추�?? : 2025 - 03 - 26 GameStart, GameOver ?��미�?? 추�?? �? 2�? 출력
     */

    /// <summary>
    /// GameStart UI ?��미�?? ( Fade In / Fade Out ?��?��?�� ?��?�� CanvasGroup ?��?��)
    /// </summary>
    public CanvasGroup gameStartCanvasGroup;

    /// <summary>
    /// GameOver UI ?��미�?? ( Fade In / Fade Out ?��?��?�� ?��?�� CanvasGroup ?��?��)
    /// </summary>
    public CanvasGroup gameOverCanvasGroup;

    /// <summary>
    /// GameOver ?��?��?�� (?���? ?�� 메인 메뉴 ?��?��, 깜빡?��?�� ?���? 추�??)
    /// </summary>
    public CanvasGroup gameOverTextCanvasGroup;

    private bool isFadingText = true; // GameOver ?��?��?�� 깜빡?���? ?���?

    /// <summary>
    /// ?��?��?�� ?��?��
    /// </summary>
    public float fadeDuration = 0.5f;
    // GameOver ?��?��?�� ?��?��?�� ?��?��
    public float gameOverTextFadeDuration = 1f;

    public bool CanClick = false;

    /*******************?��?��*********************/

    /// <summary>
    /// GameManager�� �α� �±�.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// ����� �α� ��� ON/OFF
    /// </summary>
    public bool logOn = true;

    /// <summary>
    /// �ʱ�ȭ �Լ�.
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
        ????�� 코드 추�?? : 2025 - 03 - 26 GameStart, GameOver ?��미�?? 추�?? �? 2�? 출력
        */
        //BGMManager.Instance.PlayBGM1(); -> StartGame()?���? ?���?

        StartCoroutine(ShowGameStart());

        /*******************?��?��*********************/

    }




    /// <summary>
    /// 게임 ?��?��
    /// </summary>
        /*
        ????�� 코드 추�?? : 2025 - 03 - 26 GameStart, GameOver ?��미�?? 추�?? �? 2�? 출력
        */
    private void StartGame()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] Game Start!");
        BGMManager.Instance.PlayBGM1();
    }


    /// <summary>
    /// GameStart ?��면을 ?��?��?�� ?��/?��?��?���? 보여주고 2�? ?�� 게임 ?��?��
    /// </summary>
    /// 
    IEnumerator ShowGameStart()
    {
        if (gameStartCanvasGroup != null)
        {
            // 기존 gameStartImage ?��?��?�� 코드 ????��, ?��?��?�� ?�� ?���? ?��?��
            //코루?�� ?��?�� FadeCanvasGroup(?��?��?�� 캠퍼?��그룹, ?��?�� ?��?�� �? 0 ?���? ?���?  1?���? 불투�?, ?��?�� , ?��?��?�� �??�� ?��간인?�� ?��?�� ?��?��?��?��?�� 1f, true?���? Time.timeScale = 0 ?��?��?�� ?��?��?�� ?��. ?��?��?���? ?��?���? 기본�? False)

            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 0f, 1f)); // ?��?��?�� 밝아�?
            yield return new WaitForSeconds(2f);        //2�? ?��
            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 1f, 0f)); // ?��?��?�� ?��?���?
        }

        StartGame();
    }


    public void GameOver()
    {
        /*Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        BGMManager.Instance.PlayBGM2();
        -> ShowGameOverScreen() ?��?�� 집어?��?��
         */
        PWRManager.Instance.Run = false;
        Debug.Log("Run : "+ PWRManager.Instance.Run);
        StartCoroutine(ShowGameOverScreen());   //코루?�� ?��?��
    }


    /// <summary>
    /// Game Over ?��면을 ?��?���? 게임 ?���?
    /// </summary>
    IEnumerator ShowGameOverScreen()
    {


        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        CanClick = true;
        // BGM �?�?
        BGMManager.Instance.PlayBGM2();

        // 기존 gameOverImage ?��?��?�� 코드 ????��, ?��?��?�� ?�� ?���? ?��?��
        if (gameOverCanvasGroup != null)
        {
            //코루?�� ?��?�� FadeCanvasGroup(?��?��?�� 캠퍼?��그룹, ?��?�� ?��?�� �? 0 ?���? ?���?  1?���? 불투�?, ?��?�� , ?��?��?�� �??�� ?��간인?�� ?��?�� ?��?��?��?��?�� 1f, true?���? Time.timeScale = 0 ?��?��?�� ?��?��?�� ?��. ?��?��?���? ?��?���? 기본�? False)
            yield return StartCoroutine(FadeCanvasGroup(gameOverCanvasGroup, 0f, 1f));
        }
        // GameOver ?��?��?�� 깜빡?���? ?��?��
        if (gameOverTextCanvasGroup != null)
        {
            StartCoroutine(FadeLoopGameOverText());
        }

        // 게임 멈추�?
        Time.timeScale = 0;

    }

    /// <summary>
    /// �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�.
    /// </summary>
    void Update()
    {

        /*
         ????�� 코드 추�?? : 2025 - 03 - 26 GameStart, GameOver ?��미�?? 추�?? �? 2�? 출력
         */
        if (CanClick && isFadingText && Input.GetKeyDown(KeyCode.Z))
        {
            isFadingText = false; // 코루?�� 종료 ?��?���? ?��?��
            CanClick = false;
            LoadMainMenu();
        }


        // TODO: ?��?��?��?��?�� 체력?�� 0 ?��?��?���? 게임 종료 ?��?�� 문구 ?��?���?
        // TODO: UI ?��면에 ?��?��?���?
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManager�� UI ���� �Լ��� �����ϴ� �׽�Ʈ �Լ�.
    /// </summary>
    public void changeUI()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] changeUI");
    }

    /// <summary>
    /// GameOver ?��?��?���? 계속 깜빡?��?���? 반복
    /// </summary>
    /*
    ????�� 코드 추�?? : 2025 - 03 - 26 GameStart, GameOver ?��미�?? 추�?? �? 2�? 출력
    */
    private IEnumerator FadeLoopGameOverText()
    {
        while (isFadingText)
        {
            //코루?�� ?��?�� FadeCanvasGroup(?��?��?��?�� 캠퍼?��그룹, ?��?�� ?��?�� �? 0 ?���? ?���?  1?���? 불투�?, ?��?�� , ?��?��?�� �??�� ?��간인?�� ?��?�� ?��?��?��?��?�� 1f, true?���? Time.timeScale = 0 ?��?��?�� ?��?��?�� ?��.)
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 0f, 1f, gameOverTextFadeDuration, true));
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 1f, 0f, gameOverTextFadeDuration, true));

        }
    }

    /// <summary>
    ///  메인 메뉴�? ?��?��?��?�� ?��?��
    /// </summary>
    /// 
    /*
     ????�� 코드 추�?? : 2025 - 03 - 26 GameStart, GameOver ?��미�?? 추�?? �? 2�? 출력
     */
    private void LoadMainMenu()
    {
        Time.timeScale = 1; //  ?�� �?�? ?�� TimeScale?�� ?��?�� 1�? ?��?��
        DestroyAllPersistentObjects();
        SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// CanvasGroup?�� ?��?��?�� ?��?��?�� ?���? (Unscaled Time ?��?�� �??��)
    /// </summary>
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration = -1f, bool useUnscaledTime = false)
    {
        if (duration < 0) duration = fadeDuration;      //duration?�� 기본�?(-1f)?�� 경우, ?��?��?��?�� ?��?��?�� fadeDuration 값을 ?��?��
        float elapsedTime = 0f;                                             //경과 ?���? 초기?��

        while (elapsedTime < duration)                            //?��?��?�� duration ?��?�� while 루프 ?��?�� (?��?��?�� ?���? ?��?��)
        {
            elapsedTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;               //useUnscaledTime?�� true�? Time.unscaledDeltaTime ?��?�� (게임 ?���? ?��?��?��?��?�� ?��?��) , false�? ?���? Time.deltaTime ?��?�� (게임 진행 중일 ?���? ?��?��)
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);       //Lerp ?��?���? ?��?��?��?�� alpha 값을 startAlpha ?�� endAlpha�? ?��진적?���? �?�? (보�???���? ?��보�???���? ?��?��)
            yield return null;                                                                                                                                                  // ?��?�� ?��?��?��까�?? ???�?
        }

        canvasGroup.alpha = endAlpha;
    }


    /// <summary>
    /// MainMenu Secen ?���? ?��?��갈때, DontDestroyOnLoad object ?���?
    /// </summary>
    private void DestroyAllPersistentObjects()
{
     GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);     //?��?�� ?��?�� 모든 ?��브젝?���? �??��

        foreach (GameObject obj in allObjects)  //모든 ?��브젝?���? foreach�? ?��?��?��?�� ?��?��?���? 처리
        {
            //obj.scene.handle == -1�? ?��?�� ?��브젝?���? DontDestroyOnLoad ?��?�� ?��?��?���? ?��?��
            if (obj.scene.buildIndex == -1) // DontDestroyOnLoad?�� ?��?�� ?��브젝?��?���? ?��?��
        {
                //?��?�� ?��브젝?���? ?��?��
                Destroy(obj);
        }
    }
}

    /*******************?��?��*********************/

}
