using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private const string TAG = "Singleton";

    private static T instance; // Singleton �ν��Ͻ��� ������ static ����

    /// <summary>
    /// Singleton �ν��Ͻ��� �������� �Ӽ�.
    /// �ν��Ͻ��� ������ ���� �����ϰ�, �̹� ������ ���� �ν��Ͻ��� ��ȯ.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null) // �ν��Ͻ��� null�̸� ���� ã�ų� ����
            {
                instance = FindFirstObjectByType<T>(); // ������ �ش� Ÿ���� ù ��° ������Ʈ�� ã��

                if (instance == null) // ã�� ���ϸ� ���ο� GameObject�� ���� �߰�
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name); // ���� ������Ʈ�� ���� ����
                    instance = singletonObject.AddComponent<T>(); // �ش� Ÿ���� ������Ʈ�� �߰��Ͽ� �ν��Ͻ� �Ҵ�
                    DontDestroyOnLoad(singletonObject); // �� ��ȯ �ÿ��� ������Ʈ�� �ı����� �ʵ��� ����
                }
            }
            
            return instance; // �ν��Ͻ� ��ȯ
        }
    }

    /// <summary>
    /// MonoBehaviour�� Awake �޼���.
    /// �ν��Ͻ��� �̹� �����ϸ� �ٸ� ���� ������Ʈ�� �ı��ϰ�, �׷��� ������ ���� ������Ʈ�� �̱������� ����.
    /// </summary>
    protected virtual void Awake()
    {
        if (instance != null && instance != this) // �ν��Ͻ��� �����ϰ�, ���� ������Ʈ�� �ƴ϶�� �ߺ��� �̱����̹Ƿ� �ı�
        {
            // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Destroy");
            Destroy(gameObject); // �ߺ��� �̱����� �ı�
            return;
        }

        instance = this as T; // �ν��Ͻ��� null�̸� ���� ������Ʈ�� �ν��Ͻ��� ����
        DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �� ���� ������Ʈ�� �ı����� �ʵ��� ����
    }
}
