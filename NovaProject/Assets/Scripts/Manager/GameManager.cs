using System.Collections;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 게임을 총괄하는 GameManager 클래스.
/// </summary>
public class GameManager : Singleton<GameManager>
{

    /*
     대현 코드 추가 : 2025 - 03 - 26 GameStart, GameOver 이미지 추가 및 2초 출력
     */

    /// <summary>
    /// GameStart UI 이미지 ( Fade In / Fade Out 사용을 위해 CanvasGroup 사용)
    /// </summary>
    public CanvasGroup gameStartCanvasGroup;

    /// <summary>
    /// GameOver UI 이미지 ( Fade In / Fade Out 사용을 위해 CanvasGroup 사용)
    /// </summary>
    public CanvasGroup gameOverCanvasGroup;

    /// <summary>
    /// GameOver 텍스트 (클릭 시 메인 메뉴 이동, 깜빡이는 효과 추가)
    /// </summary>
    public CanvasGroup gameOverTextCanvasGroup;

    private bool isFadingText = true; // GameOver 텍스트 깜빡이기 여부

    /// <summary>
    /// 페이드 속도
    /// </summary>
    public float fadeDuration = 0.5f;
    // GameOver 텍스트 페이드 속도
    public float gameOverTextFadeDuration = 1f;

    public bool CanClick = false;

    /*******************수정*********************/

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
    /// Start
    /// </summary>
    private void Start()
    {

        /*
        대현 코드 추가 : 2025 - 03 - 26 GameStart, GameOver 이미지 추가 및 2초 출력
        */
        //BGMManager.Instance.PlayBGM1(); -> StartGame()으로 옮김

        StartCoroutine(ShowGameStart());

        /*******************수정*********************/

    }




    /// <summary>
    /// 게임 시작
    /// </summary>
        /*
        대현 코드 추가 : 2025 - 03 - 26 GameStart, GameOver 이미지 추가 및 2초 출력
        */
    private void StartGame()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] Game Start!");
        BGMManager.Instance.PlayBGM1();
    }


    /// <summary>
    /// GameStart 화면을 페이드 인/아웃하며 보여주고 2초 후 게임 시작
    /// </summary>
    /// 
    IEnumerator ShowGameStart()
    {
        if (gameStartCanvasGroup != null)
        {
            // 기존 gameStartImage 활성화 코드 대신, 페이드 인 효과 적용
            //코루틴 시작 FadeCanvasGroup(사용할 캠퍼스그룹, 시작 알파 값 0 이면 투명  1이면 불투명, 동일 , 페이드 지속 시간인데 위에 정의해놓음 1f, true이면 Time.timeScale = 0 이여도 동작을 함. 적어놓지 않으면 기본값 False)

            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 0f, 1f)); // 서서히 밝아짐
            yield return new WaitForSeconds(2f);        //2초 뒤
            yield return StartCoroutine(FadeCanvasGroup(gameStartCanvasGroup, 1f, 0f)); // 서서히 사라짐
        }

        StartGame();
    }


    public void GameOver()
    {
        /*Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        BGMManager.Instance.PlayBGM2();
        -> ShowGameOverScreen() 안에 집어넣음
         */

        StartCoroutine(ShowGameOverScreen());   //코루틴 실행
    }


    /// <summary>
    /// Game Over 화면을 띄우고 게임 정지
    /// </summary>
    IEnumerator ShowGameOverScreen()
    {


        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
        CanClick = true;
        // BGM 변경
        BGMManager.Instance.PlayBGM2();

        // 기존 gameOverImage 활성화 코드 대신, 페이드 인 효과 적용
        if (gameOverCanvasGroup != null)
        {
            //코루틴 시작 FadeCanvasGroup(사용할 캠퍼스그룹, 시작 알파 값 0 이면 투명  1이면 불투명, 동일 , 페이드 지속 시간인데 위에 정의해놓음 1f, true이면 Time.timeScale = 0 이여도 동작을 함. 적어놓지 않으면 기본값 False)
            yield return StartCoroutine(FadeCanvasGroup(gameOverCanvasGroup, 0f, 1f));
        }
        // GameOver 텍스트 깜빡이기 시작
        if (gameOverTextCanvasGroup != null)
        {
            StartCoroutine(FadeLoopGameOverText());
        }

        // 게임 멈추기
        Time.timeScale = 0;

    }

    /// <summary>
    /// 프레임마다 호출되는 업데이트 함수.
    /// </summary>
    void Update()
    {

        /*
         대현 코드 추가 : 2025 - 03 - 26 GameStart, GameOver 이미지 추가 및 2초 출력
         */
        if (CanClick && isFadingText && Input.GetMouseButtonDown(0))
        {
            isFadingText = false; // 🔹 코루틴 종료 플래그 설정
            CanClick = false;
            LoadMainMenu();
        }


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

    /// <summary>
    /// GameOver 텍스트가 계속 깜빡이도록 반복
    /// </summary>
    /*
    대현 코드 추가 : 2025 - 03 - 26 GameStart, GameOver 이미지 추가 및 2초 출력
    */
    private IEnumerator FadeLoopGameOverText()
    {
        while (isFadingText)
        {
            //코루틴 시작 FadeCanvasGroup(사용하ㄹ 캠퍼스그룹, 시작 알파 값 0 이면 투명  1이면 불투명, 동일 , 페이드 지속 시간인데 위에 정의해놓음 1f, true이면 Time.timeScale = 0 이여도 동작을 함.)
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 0f, 1f, gameOverTextFadeDuration, true));
            yield return StartCoroutine(FadeCanvasGroup(gameOverTextCanvasGroup, 1f, 0f, gameOverTextFadeDuration, true));

        }
    }

    /// <summary>
    ///  메인 메뉴로 이동하는 함수
    /// </summary>
    /// 
    /*
     대현 코드 추가 : 2025 - 03 - 26 GameStart, GameOver 이미지 추가 및 2초 출력
     */
    private void LoadMainMenu()
    {
        Time.timeScale = 1; //  씬 변경 전 TimeScale을 다시 1로 설정
        DestroyAllPersistentObjects();
        SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// CanvasGroup을 활용한 페이드 효과 (Unscaled Time 적용 가능)
    /// </summary>
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration = -1f, bool useUnscaledTime = false)
    {
        if (duration < 0) duration = fadeDuration;      //duration이 기본값(-1f)일 경우, 클래스에 정의된 fadeDuration 값을 사용
        float elapsedTime = 0f;                                             //경과 시간 초기화

        while (elapsedTime < duration)                            //설정한 duration 동안 while 루프 실행 (페이드 효과 적용)
        {
            elapsedTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;               //useUnscaledTime이 true면 Time.unscaledDeltaTime 사용 (게임 정지 상태에서도 동작) , false면 일반 Time.deltaTime 사용 (게임 진행 중일 때만 동작)
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);       //Lerp 함수를 사용하여 alpha 값을 startAlpha → endAlpha로 점진적으로 변경 (보였다가 안보였다가 점등)
            yield return null;                                                                                                                                                  // 다음 프레임까지 대기
        }

        canvasGroup.alpha = endAlpha;
    }


    /// <summary>
    /// MainMenu Secen 으로 넘어갈때, DontDestroyOnLoad object 제거
    /// </summary>
    private void DestroyAllPersistentObjects()
{
     GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);     //씬에 있는 모든 오브젝트를 검색

        foreach (GameObject obj in allObjects)  //모든 오브젝트를 foreach를 사용하여 순회하며 처리
        {
            //obj.scene.handle == -1로 해당 오브젝트가 DontDestroyOnLoad 씬에 속하는지 확인
            if (obj.scene.buildIndex == -1) // DontDestroyOnLoad에 있는 오브젝트인지 확인
        {
                //해당 오브젝트를 삭제
                Destroy(obj);
        }
    }
}

    /*******************수정*********************/

}
