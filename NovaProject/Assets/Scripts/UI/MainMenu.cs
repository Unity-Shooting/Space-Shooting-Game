using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image StartButton;
    public Image ExitButton;
    //버튼 추가
    public Image HowToPlayButton;
    public Image CreaditButton;

    public Image RealExit;
    public Image SelectStage;
    //판넬 추가
    public Image HowToPlayPanel;
    public Image CreditPanel;

    public Image RealExitButton;
    public Image RollBackButton;

    public Image StageOne;
    public Image StageTwo;
    public Image StageHidden;
    public Image Cancel;

    //Panel Cancel 버튼 추가
    public Image HowToPlayCancel;
    public Image CreditCancel;

    void Start()
    {
        RealExit.gameObject.SetActive(false);   //Exit 버튼을 누르면,종료확인 창이 하나 뜨는데, 기본값 False 주고, Exit 버튼 누를시, True
        SelectStage.gameObject.SetActive(false);   //Exit 버튼을 누르면,종료확인 창이 하나 뜨는데, 기본값 False 주고, Exit 버튼 누를시, True
        HowToPlayPanel.gameObject.SetActive(false);   //Exit 버튼을 누르면,종료확인 창이 하나 뜨는데, 기본값 False 주고, Exit 버튼 누를시, True
        CreditPanel.gameObject.SetActive(false);   //Exit 버튼을 누르면,종료확인 창이 하나 뜨는데, 기본값 False 주고, Exit 버튼 누를시, True
        StartButton.GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        ExitButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
        RealExitButton.GetComponent<Button>().onClick.AddListener(OnRealExitButtonClick);
        RollBackButton.GetComponent<Button>().onClick.AddListener(OnRollBackButtonClick);
        StageOne.GetComponent<Button>().onClick.AddListener(OnStageOneButtonClick);
        StageTwo.GetComponent<Button>().onClick.AddListener(OnStageTwoButtonClick);
        StageHidden.GetComponent<Button>().onClick.AddListener(OnStageHiddenButtonClick);
        Cancel.GetComponent<Button>().onClick.AddListener(OnCancelButtonClick);
        HowToPlayButton.GetComponent<Button>().onClick.AddListener(OnHowToPlayButtonClick);
        CreaditButton.GetComponent<Button>().onClick.AddListener(OnCreaditButtonClick);
        HowToPlayCancel.GetComponent<Button>().onClick.AddListener(OnHowToPlayCancelClick);
        CreditCancel.GetComponent<Button>().onClick.AddListener(OnCreditCancelClick);



    }

    void OnStartButtonClick()
    {
        //SceneManager.LoadScene("OneStageScene"); //1스테이지 시작   추후 만들어지면 주석 풀 예정
        //SceneManager.LoadScene("StageOne");
        SelectStage.gameObject.SetActive(true);
    }
    void OnHowToPlayButtonClick()
    {
        HowToPlayPanel.gameObject.SetActive(true);
    }
    void OnCreaditButtonClick()
    {
        CreditPanel.gameObject.SetActive(true);
    }
    void OnHowToPlayCancelClick()
    {
        HowToPlayPanel.gameObject.SetActive(false);
    }
    void OnCreditCancelClick()
    {
        CreditPanel.gameObject.SetActive(false);
    }

    void OnStageOneButtonClick()
    {
        SceneManager.LoadScene("StageOne");
        SelectStage.gameObject.SetActive(false); 

    }
    void OnStageTwoButtonClick()
    {
        SceneManager.LoadScene("StageTwo");
        SelectStage.gameObject.SetActive(false);

    }
    void OnStageHiddenButtonClick()
    {
        SceneManager.LoadScene("StageHidden");
        SelectStage.gameObject.SetActive(false);

    }
    void OnCancelButtonClick()
    {
        SelectStage.gameObject.SetActive(false);
    }

    void OnExitButtonClick()
    {
        RealExit.gameObject.SetActive(true);    //Exit 버튼을 클릭 시, 종료확인 창 True;
    }

    void OnRealExitButtonClick()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();   //추후에 빌드가 되어지면 해당 코드로 변경 예정
    }
    
    void OnRollBackButtonClick()
    {
        RealExit.gameObject.SetActive(false);
    }

}
