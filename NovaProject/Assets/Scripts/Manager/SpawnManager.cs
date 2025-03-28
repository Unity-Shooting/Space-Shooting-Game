using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// 몬스터 한 개체의 실행에 관한 정보, 반복도 가능함!
/// </summary>
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
    public float type;

    [Header("반복 설정")]
    [Tooltip("몇 번 반복할지")]
    public int repeatCount = 1;

    [Tooltip("반복 간격 (초)")]
    public float repeatInterval = 1f;
}

/// <summary>
/// 하나의 Wave에 대한 정보
/// SpawnTimelineSO 안에는 여러개의 SpawnEventData의 리스트가 들어있음
/// 이 스테이지지가 시작하고 startTime 후에, SpawnTileline에 들어있는 소환이벤트들을 시간 순서대로 실행
/// </summary>
[System.Serializable]
public class WaveData
{
    public int startTime;
    public SpawnTimelineSO timeline;
}

/// <summary>
/// 전체적으로 몬스터 스폰을 관리함
/// 싱글톤형태로 게임매니저에서 각 스테이지가 시작할때
/// StartStage(stageindex) 해주면 끝!
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private MonsterBoss boss;
    [SerializeField] private List<StageWaveSO> stages = new();  // 하나의 스테이지동안 실행될 여러 WavaData의 리스트를 가지고있음

    void Start()
    {
        StartStage(1);   // 테스트용 코드. 1스테이에 저장된 정보 바로 실행
    }
    /// <summary>
    /// 해당 스테이지의 스폰을 시작
    /// </summary>
    /// <param name="i"></param>
    void StartStage(int stageIndex)
    {
        if (stageIndex < 1 || stageIndex > stages.Count)
            Debug.LogWarning("스테이지 인덱스 오류");
        StartCoroutine(WaveStarter(stages[stageIndex - 1].waves));
    }

    /// <summary>
    /// 한 스테이지는 여러개의 wave로 구성
    /// 각 wave에 지정된 startTime이 지나면 해당 wave를 실행함
    /// </summary>
    /// <param name="waves"></param>
    /// <returns></returns>
    IEnumerator WaveStarter(List<WaveData> waves)
    {
        float currentTime = 0;  // 시간체크
        int i = 0;

        // 휴먼에러 방지를 위해 시간 오름차순으로 정렬해줌
        waves.Sort((x, y) => x.startTime.CompareTo(y.startTime));


        while (i < waves.Count)
        {
            currentTime += Time.deltaTime; // 현재 시간 구하기

            var wave = waves[i];
            if (currentTime >= wave.startTime)
            {
                StartCoroutine(SpawnWave(wave.timeline)); // 해당 wave 스폰 시작!
                i++;
            }
            yield return null;
        }
    }

    public IEnumerator SpawnWave(SpawnTimelineSO timeline)
    {
        timeline.spawnEvents.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime)); // 오름차순 정렬로 혹시모를 휴먼에러 대비
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


    /// <summary>
    /// 몬스터프리펩, 스폰위치, 초기진행방향, 몬스터 행동타입
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// 이 함수를 만든 이유는 오브젝트 풀링 특성상 한번 쓰고난 오브젝트는 위치값이 이상하게 들어가 있어서
    /// 반드시 위치값을 넣어줘야하는데(Instantiate처럼 프리펩 초기값으로 생성되지 않음)
    /// 언젠가 한번은 까먹고 안할것같아서 함수로 묶어뒀습니다
    private void SpawnMonster(GameObject monster, Vector3 pos, Vector2 dir, float type)
    {
        var go = PoolManager.instance.Get(monster);    // 오브젝트 풀링에서 생성 Instantiate대신 쓴다고 생각하면 될것같습니다
        go.GetComponent<Monster>().Init(pos, dir, type); // 몬스터 안에서 OnEnable로 초기화 할 수 있긴 하지만
                                                         // 스폰위치, 진행방향을 생성하면서 줄 수 있어야 다양한 경로로
                                                         // 몬스터를 보낼 수 있을것 같아서 스폰매니저에서 위치 방향 지정

    }
}
