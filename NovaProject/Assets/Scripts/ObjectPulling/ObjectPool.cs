using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    // 프리팹과 초기 크기를 받아 풀 초기화:

    // 생성자에서 프리팹과 초기 크기를 받아 풀을 초기화합니다.
    // 초기 크기만큼 오브젝트를 생성하여 풀에 추가합니다.
    // 새로운 오브젝트를 생성하여 풀에 추가:

    // CreateNewObject 메서드는 새로운 오브젝트를 생성하고 비활성화 상태로 풀에 추가합니다.
    // 풀에서 사용 가능한 오브젝트를 가져오기:

    // Get 메서드는 풀에서 사용 가능한 오브젝트를 가져옵니다.
    // 풀이 비어있으면 새로운 오브젝트를 생성합니다.
    // 가져온 오브젝트를 활성화 상태로 반환합니다.
    // 사용이 끝난 오브젝트를 풀로 반환:

    // Return 메서드는 사용이 끝난 오브젝트를 비활성화 상태로 풀에 반환합니다.
    // 이 클래스는 오브젝트 풀링을 통해 오브젝트 생성과 파괴에 따른 성능 저하를 줄이고, 메모리 관리를 효율적으로 할 수 있도록 도와줍니다.

    // 풀링 할 프리펩
    private GameObject prefab;

    // 비황성화된 오브젝트들을 보관하는 큐
    private Queue<GameObject> pool;

    // 풀링된 오브젝트들의 부모 트랜스폼
    private Transform parent;

    // ,생성자 : 프리팹과 초기 크기를 받아 풀 초기화

    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    // 새로운 오브젝트를 생성하여 풀에 추가하는 private 메서드
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // 풀에서 사용 가능한 오브젝트를 가져오는 메서드
    // 풀이 비어있으면 새로 생성
    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    // 사용이 끝난 오브젝트를 풀로 반환하는 메서드
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

}
