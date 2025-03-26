using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image StartButton;
    public Image ExitButton;
    public Image RealExit;

    public Image RealExitButton;
    public Image RollBackButton;
   

    void Start()
    {
        RealExit.gameObject.SetActive(false);   //Exit 버튼을 누르면,종료확인 창이 하나 뜨는데, 기본값 False 주고, Exit 버튼 누를시, True
        StartButton.GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        ExitButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
        RealExitButton.GetComponent<Button>().onClick.AddListener(OnRealExitButtonClick);
        RollBackButton.GetComponent<Button>().onClick.AddListener(OnRollBackButtonClick);
    }

    void OnStartButtonClick()
    {
        Debug.Log("띵동 : ");     //테스트용
        //SceneManager.LoadScene("OneStageScene"); //1스테이지 시작   추후 만들어지면 주석 풀 예정
        SceneManager.LoadScene("StageOne");
    }

    void OnExitButtonClick()
    {
        RealExit.gameObject.SetActive(true);    //Exit 버튼을 클릭 시, 종료확인 창 True;
    }

    void OnRealExitButtonClick()
    {
        Debug.Log("진짜 끔 : ");   //테스트용
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();   //추후에 빌드가 되어지면 해당 코드로 변경 예정
    }
    
    void OnRollBackButtonClick()
    {
        Debug.Log("안끔 : ");   //테스트용
        RealExit.gameObject.SetActive(false);
    }

}
