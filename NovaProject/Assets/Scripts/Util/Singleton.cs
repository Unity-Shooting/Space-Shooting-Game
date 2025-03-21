using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private const string TAG = "Singleton";

    // Singleton �ν��Ͻ��� ������ static ����
    private static T instance;

    /// <summary>
    /// Singleton �ν��Ͻ��� �������� �Ӽ�.
    /// �ν��Ͻ��� ������ ���� �����ϰ�, �̹� ������ ���� �ν��Ͻ��� ��ȯ.
    /// </summary>
    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� null�̸� ���� ã�ų� ����
            if (instance == null)
            {
                // ������ �ش� Ÿ���� ù ��° ������Ʈ�� ã��
                instance = FindFirstObjectByType<T>();

                // ã�� ���ϸ� ���ο� GameObject�� ���� �߰�
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name); // ���� ������Ʈ�� ���� ����
                    instance = singletonObject.AddComponent<T>(); // �ش� Ÿ���� ������Ʈ�� �߰��Ͽ� �ν��Ͻ� �Ҵ�
                    DontDestroyOnLoad(singletonObject); // �� ��ȯ �ÿ��� ������Ʈ�� �ı����� �ʵ��� ����
                }
            }
            // �ν��Ͻ� ��ȯ
            return instance;
        }
    }

    /// <summary>
    /// MonoBehaviour�� Awake �޼���.
    /// �ν��Ͻ��� �̹� �����ϸ� �ٸ� ���� ������Ʈ�� �ı��ϰ�, �׷��� ������ ���� ������Ʈ�� �̱������� ����.
    /// </summary>
    protected virtual void Awake()
    {
        // �ν��Ͻ��� �����ϰ�, ���� ������Ʈ�� �ƴ϶�� �ߺ��� �̱����̹Ƿ� �ı�
        if (instance != null && instance != this)
        {
            // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Destroy");
            Destroy(gameObject); // �ߺ��� �̱����� �ı�
            return;
        }

        // �ν��Ͻ��� null�̸� ���� ������Ʈ�� �ν��Ͻ��� ����
        instance = this as T;
        DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �� ���� ������Ʈ�� �ı����� �ʵ��� ����
    }
}
