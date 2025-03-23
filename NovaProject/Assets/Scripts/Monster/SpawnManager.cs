using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class SpawnEventData
{

    [Header("기본 설정")]
    [Tooltip("이 이벤트가 실행될 시간 (초 단위)")]
    public float spawnTime;

    [Tooltip("스폰될 몬스터 프리팹")]
    public GameObject monster;

    [Tooltip("스폰 위치")]
    public Vector3 position;

    [Tooltip("초기 이동 방향")]
    public Vector2 direction;

    [Tooltip("몬스터 타입 / 패턴 분기용")]
    public int type;

    [Header("반복 설정")]
    [Tooltip("몇 번 반복할지")]
    public int repeatCount = 1;

    [Tooltip("반복 간격 (초)")]
    public float repeatInterval = 1f;
}

[System.Serializable]
public class WaveData
{
    public int startTime;
    public SpawnTimelineSO timeline;
}

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<StageWaveSO> stages = new();
    
    void Start()
    {
        StartSpawn(1);   
    }
    /// <summary>
    /// i : 스테이지 넘버
    /// </summary>
    /// <param name="i"></param>
    void StartSpawn(int stageIndex)
    {
        if (stageIndex < 1 || stageIndex > 4)
            Debug.LogWarning("스테이지 인덱스 오류");
        StartCoroutine(WaveStarter(stages[stageIndex-1].waves));
    }

    IEnumerator WaveStarter(List<WaveData> waves)
    {
        float currentTime = 0;
        int i = 0;
        waves.Sort( (x,y) => x.startTime.CompareTo(y.startTime) );
        while (i < waves.Count)
        {
            currentTime += Time.deltaTime;

            var wave = waves[i];
            if (currentTime >= wave.startTime)
            {
                StartCoroutine(SpawnWave(wave.timeline));
                i++;
            }
            yield return null;
        }
    }

    IEnumerator SpawnWave(SpawnTimelineSO timeline)
    {
        timeline.spawnEvents.Sort((a,b) => a.spawnTime.CompareTo(b.spawnTime)); // 오름차순 정렬로 혹시모를 휴먼에러 대비
        float currentTime = 0;
        int i = 0;
        while (i < timeline.spawnEvents.Count)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeline.spawnEvents[i].spawnTime)
            {
                StartCoroutine(RunSpawnEvent(timeline.spawnEvents[i]));
                i++;
            }
            yield return null;
        }

        
    }

    IEnumerator RunSpawnEvent(SpawnEventData eventData)
    {
        int count = eventData.repeatCount;
        if (count < 1)
        {
            count = 1;
        }
        for (int i = 0; i < count; i++)
        {
            SpawnMonster(eventData.monster, eventData.position, eventData.direction, eventData.type);
            if (i == eventData.repeatCount - 1)
                break;
            yield return new WaitForSeconds(eventData.repeatInterval);
        }

    }



    private void SpawnMonster(GameObject monster, Vector3 pos, Vector2 dir, int type)
    {
        var go = PoolManager.instance.Get(monster);    // 오브젝트 풀링에서 생성 Instantiate대신 쓴다고 생각하면 될것같습니다
        Debug.Log($"스폰매니저의 스폰몬스터 {pos}");
        go.GetComponent<Monster>().Init(pos, dir, type); // 몬스터 안에서 OnEnable로 초기화 할 수 있긴 하지만
                                                         // 스폰위치, 진행방향을 생성하면서 줄 수 있어야 다양한 경로로
                                                         // 몬스터를 보낼 수 있을것 같아서 스폰매니저에서 위치 방향 지정

    }
}
