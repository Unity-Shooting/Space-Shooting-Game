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
        RealExit.gameObject.SetActive(false);   //Exit ��ư�� ������,����Ȯ�� â�� �ϳ� �ߴµ�, �⺻�� False �ְ�, Exit ��ư ������, True
        StartButton.GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        ExitButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
        RealExitButton.GetComponent<Button>().onClick.AddListener(OnRealExitButtonClick);
        RollBackButton.GetComponent<Button>().onClick.AddListener(OnRollBackButtonClick);
    }

    void OnStartButtonClick()
    {
        Debug.Log("�� : ");     //�׽�Ʈ��
        //SceneManager.LoadScene("OneStageScene"); //1�������� ����   ���� ��������� �ּ� Ǯ ����
        SceneManager.LoadScene("StageOne");
    }

    void OnExitButtonClick()
    {
        RealExit.gameObject.SetActive(true);    //Exit ��ư�� Ŭ�� ��, ����Ȯ�� â True;
    }

    void OnRealExitButtonClick()
    {
        Debug.Log("��¥ �� : ");   //�׽�Ʈ��
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();   //���Ŀ� ���尡 �Ǿ����� �ش� �ڵ�� ���� ����
    }
    
    void OnRollBackButtonClick()
    {
        Debug.Log("�Ȳ� : ");   //�׽�Ʈ��
        RealExit.gameObject.SetActive(false);
    }

}
