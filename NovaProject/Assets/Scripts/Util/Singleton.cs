using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private const string TAG = "Singleton";

    // Singleton 인스턴스를 저장할 static 변수
    private static T instance;

    /// <summary>
    /// Singleton 인스턴스를 가져오는 속성.
    /// 인스턴스가 없으면 새로 생성하고, 이미 있으면 기존 인스턴스를 반환.
    /// </summary>
    public static T Instance
    {
        get
        {
            // 인스턴스가 null이면 새로 찾거나 생성
            if (instance == null)
            {
                // 씬에서 해당 타입의 첫 번째 오브젝트를 찾음
                instance = FindFirstObjectByType<T>();

                // 찾지 못하면 새로운 GameObject를 만들어서 추가
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name); // 게임 오브젝트를 새로 생성
                    instance = singletonObject.AddComponent<T>(); // 해당 타입의 컴포넌트를 추가하여 인스턴스 할당
                    DontDestroyOnLoad(singletonObject); // 씬 전환 시에도 오브젝트가 파괴되지 않도록 설정
                }
            }
            // 인스턴스 반환
            return instance;
        }
    }

    /// <summary>
    /// MonoBehaviour의 Awake 메서드.
    /// 인스턴스가 이미 존재하면 다른 게임 오브젝트를 파괴하고, 그렇지 않으면 현재 오브젝트를 싱글톤으로 설정.
    /// </summary>
    protected virtual void Awake()
    {
        // 인스턴스가 존재하고, 현재 오브젝트가 아니라면 중복된 싱글톤이므로 파괴
        if (instance != null && instance != this)
        {
            // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Destroy");
            Destroy(gameObject); // 중복된 싱글톤을 파괴
            return;
        }

        // 인스턴스가 null이면 현재 오브젝트를 인스턴스로 설정
        instance = this as T;
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 이 게임 오브젝트가 파괴되지 않도록 설정
    }
}
