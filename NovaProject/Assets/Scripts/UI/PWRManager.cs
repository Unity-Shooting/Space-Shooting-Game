using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PWRManager : MonoBehaviour
{

    // 싱글턴 인스턴스
    public static PWRManager Instance { get; private set; }

    public Image Pwr;
    public Image Ammo;
    public Image[] images;
    public Image Pause;
    public GameObject PauseMenu;

    public Button Resume;
    public Button MainMenu;

    public bool Run  { get; set; }

    public float duration;

    private void Awake()
    {
        Run = true;
        // 만약 이미 인스턴스가 존재하면, 현재 오브젝트를 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스를 자신으로 설정
        Instance = this;
    }

    void Start()
    {

            if (Run)
            /*
             * Run에 대한 설명
             * Run은 게임이 실행되고 있을때, 스테이지가 진행 중이며 캐릭터가 GameOver가 되지 않을 때, Run은 true 상태이다.
             * True 상태일땐, 클릭이 가능하며, Pause 버튼을 누를 수 있다.
             * 단, GameOver가 되면 Run은 False 상태가 되며 무조건 Z키를 눌러서 메인메뉴로 이동해야하며, 이때 Pause 버튼 클릭이 안된다.
             * Run이 false상태이기 때문!
             * 한줄 요약 : 마우스 클릭 동작 True False
             */
        {
            Debug.Log("Run : "+Run);
            Pause.GetComponent<Button>().onClick.AddListener(PauseAction);
        }
        Resume.onClick.AddListener(OnResume);
        MainMenu.onClick.AddListener(OnMainMenu);

        duration = PlayerController.Instance.shieldCoolTime;

        //오류 로그로 인한 수정
        if (images == null || images.Length < 3)
        {
            Debug.LogError($"크기가 부족합니다! (현재 크기: {images.Length})");
            return; 
        }

        HideAllImages();
        ShowImage(1);
        // ScoreManager.instance.AddScore(100); ScoreManager 인스턴스 테스트
    }

    public void PauseAction()
    {
        if (Run && Time.timeScale == 1)  // 게임이 진행 중일 때
        {
            Debug.Log("PauseMenu 클릭 : ");
            Time.timeScale = 0; // 게임을 일시 정지
            PauseMenu.SetActive(true); // Pause 메뉴 표시
        }
        else if (Run && Time.timeScale == 0) // 게임이 일시 정지 중일 때
        {
            OnResume(); // 일시 정지 상태에서 Resume
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);

    }
    public void OnMainMenu()
    {
        Time.timeScale = 1;
        LoadMainMenu();
    }

    /// <summary>
    ///  메인 메뉴로 이동하는 함수
    /// </summary>
    /// 
    private void LoadMainMenu()
    {
        Time.timeScale = 1; //  씬 변경 전 TimeScale을 다시 1로 설정
        DestroyAllPersistentObjects();
        SceneManager.LoadScene("MainMenu");
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

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I키 누르는중 : ");
            ScoreManager.instance.AddCheckScore(1);
        }

        if (SceneManager.GetActiveScene().name == "StageHidden")
            {
            // 'StageHidden' 씬일 때 실행할 코드
            if (Input.GetKeyDown(KeyCode.Alpha1)) ShowImage(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ShowImage(2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) ShowImage(3);

        }
        else { 

        if (Input.GetKeyDown(KeyCode.LeftShift) && Pwr.fillAmount == 1f)   //실드 게이지 UI 갱신 10초 쿨타임
        {
            StartCoroutine(FillOverTime(duration));
        }
        if (Input.GetKeyDown(KeyCode.B) && Ammo.fillAmount == 1f)   // B를 눌렀을때 , Ammo 게이지가 1f일때,
        {
            if (images[0].gameObject.activeSelf && WeaponManager.Instance.MissileUnlocked)  //0번 이미지 Active일때, UI창에 미사일인 경우 && 미사일 해금 했을 경우
            {
                StartCoroutine(FillOverTimeAmmo(duration));
            }
            else if (images[1].gameObject.activeSelf && WeaponManager.Instance.LaserUnlocked) //1번 이미지 Active일때, UI창에 레이저인 경우 && 레이저 해금 했을 경우
            {
                StartCoroutine(FillOverTimeAmmo(duration));
            }
            else if (images[2].gameObject.activeSelf && WeaponManager.Instance.BombUnlocked) //2번 이미지 Active일때, UI창에 폭탄인 경우 && 폭탄 해금 했을 경우
            {
                StartCoroutine(FillOverTimeAmmo(duration));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) ShowImage(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ShowImage(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ShowImage(3);
        }

    }

    public void ShowImage(int index)
    {

        if (images == null || index < 1 || index > images.Length)
        {
            Debug.LogWarning($"잘못된 인덱스 접근: {index}, images 배열 크기: {images.Length}");
            return;
        }

        HideAllImages();

        images[index - 1].gameObject.SetActive(true); //  인덱스 조정 (0부터 시작)
        Debug.Log("Index : "+index +" 번 이미지");

        //오류 로그로 인한 수정

        /*HideAllImages(); // 모든 이미지를 비활성화

        switch (index)
        {
            case 1:
                images[0].gameObject.SetActive(true);
                break;
            case 2:
                images[1].gameObject.SetActive(true);
                break;
            case 3:
                images[2].gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("잘못된 번호입니다.");
                break;
        }*/
    }

    void HideAllImages()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
    }

    IEnumerator FillOverTime(float time)
    {
        Pwr.fillAmount = 0f;
        float elapsedTime = 0f; // 경과 시간 초기화
        while (elapsedTime < time)
        {
            Pwr.fillAmount = elapsedTime / time; // FillAmount 비율 계산
            elapsedTime += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
        Pwr.fillAmount = 1f; // 10초가 되면 fillAmount를 정확히 1로 설정
    }

    IEnumerator FillOverTimeAmmo(float time)
    {
        Ammo.fillAmount = 0f;
        float elapsedTime = 0f; // 경과 시간 초기화
        while (elapsedTime < time)
        {
            Ammo.fillAmount = elapsedTime / time; // FillAmount 비율 계산
            elapsedTime += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
        Ammo.fillAmount = 1f; // 10초가 되면 fillAmount를 정확히 1로 설정
    }

    public void StartPower()
    {
        Run = true;
    }

    public void StopPower()
    {
        Run = false;
    }

}
