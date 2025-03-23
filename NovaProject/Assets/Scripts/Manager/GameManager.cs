using UnityEngine;

/// <summary>
/// ������ �Ѱ��ϴ� GameManager Ŭ����.
/// </summary>
public class GameManager : Singleton<GameManager>
{
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
    /// �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�.
    /// </summary>
    void Update()
    {
        // TODO: �÷��̾��� ü���� 0 �����̸� ���� ���� ���� ���� ����
        // TODO: UI ȭ�鿡 ǥ���ϱ�
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] health : {PlayerHealth.Instance.hp}");
    }

    /// <summary>
    /// GameManager�� UI ���� �Լ��� �����ϴ� �׽�Ʈ �Լ�.
    /// </summary>
    public void changeUI()
    {
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] changeUI");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] GameOver");
    }
}
