using System.Collections;
using UnityEngine;


// 적 스폰 관리
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float spawntimer;

    [SerializeField]
    private GameObject monsterA;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() // 프리펩 잘 됐는지 테스트용 스폰
    {
        while (true) // 스폰타이머 간격으로 monsterA 소환
        {
            yield return new WaitForSeconds(spawntimer);
            SpawnMonster(monsterA,transform.position,Vector2.down);
            Debug.Log(transform.position);
        }
    }
    /// <summary>
    /// 몬스터프리펩, 스폰위치, 초기진행방향
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// 이 함수를 만든 이유는 오브젝트 풀링 특성상 한번 쓰고난 오브젝트는 위치값이 이상하게 들어가 있어서
    /// 반드시 위치값을 넣어줘야하는데(Instantiate처럼 프리펩 초기값으로 생성되지 않음)
    /// 언젠가 한번은 까먹고 안할것같아서 함수로 묶어뒀습니다
    private void SpawnMonster(GameObject monster, Vector3 pos, Vector2 dir) 
    {
        var go = PoolManager.instance.Get(monster);    // 오브젝트 풀링에서 생성 Instantiate대신 쓴다고 생각하면 될것같습니다
        Debug.Log($"스폰매니저의 스폰몬스터 {pos}");
        go.GetComponent<Monster>().Init(pos, dir); // 몬스터 안에서 OnEnable로 초기화 할 수 있긴 하지만
                                                                           // 스폰위치, 진행방향을 생성하면서 줄 수 있어야 다양한 경로로
                                                                           // 몬스터를 보낼 수 있을것 같아서 스폰매니저에서 위치 방향 지정

    }
}
