using UnityEngine;

/// <summary>
/// ������ �Ѱ��ϴ� GameManager Ŭ����.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// GameManager�� �α� �±�.
    /// </summary>
    private const string TAG = "GameManager";

    /// <summary>
    /// �÷��̾� ��Ʈ�ѷ� ����.
    /// </summary>
    public PlayerController playerController;

    /// <summary>
    /// ���� ���� �� ����Ǵ� �ʱ�ȭ �Լ�.
    /// </summary>
    void Start()
    {
        playerController.Init(this);
    }

    /// <summary>
    /// �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�.
    /// </summary>
    void Update()
    {
        // TODO: �÷��̾��� ü���� 0 �����̸� ���� ���� ���� ���� ����
        // TODO: UI ȭ�鿡 ǥ���ϱ�
        Debug.Log(playerController.health);
    }

    /// <summary>
    /// Player���� GameManager�� UI ���� �Լ��� �����ϴ� �׽�Ʈ �Լ�.
    /// </summary>
    public void changeUI()
    {
        Debug.Log($"[{TAG}] changeUI");
    }
}
