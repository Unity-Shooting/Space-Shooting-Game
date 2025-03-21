using UnityEngine;

/// <summary>
/// �÷��̾��� ü���� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    private const string TAG = "Health";

    /// <summary>
    /// �÷��̾��� ü���� �����ϴ� �����Դϴ�.
    /// </summary>
    public int hp;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �ʱ�ȭ�� �� ȣ��˴ϴ�.
    /// <para>Awake()���� GameManager�� �α� ���¸� üũ�ϰ� �α׸� ����� �� �ֽ��ϴ�.</para>
    /// </summary>
    protected override void Awake()
    {
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake before base.Awake");
        base.Awake();
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake after base.Awake");
    }

    /// <summary>
    /// �÷��̾ �������� ���� �� ȣ��Ǵ� �޼����Դϴ�.
    /// <para>ü���� �����ϰ�, �α׷� ü�� ���¸� ����մϴ�.</para>
    /// </summary>
    /// <param name="num">�Էµ� ������ ��ġ</param>
    public void TakeDamage(int num)
    {
        // ü�� ���� �� �ּҰ� 0���� ����
        if (--hp < 1) hp = 0;
        // ü�� ���� �� �α� ���
        if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] hp : {hp}");
    }

    void Update()
    {
    }
}
