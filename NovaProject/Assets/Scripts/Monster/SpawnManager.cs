using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class SpawnEventData
{

    [Header("�⺻ ����")]
    [Tooltip("�� �̺�Ʈ�� ����� �ð� (�� ����)")]
    public float spawnTime;

    [Tooltip("������ ���� ������")]
    public GameObject monster;

    [Tooltip("���� ��ġ")]
    public Vector3 position;

    [Tooltip("�ʱ� �̵� ����")]
    public Vector2 direction;

    [Tooltip("���� Ÿ�� / ���� �б��")]
    public int type;

    [Header("�ݺ� ����")]
    [Tooltip("�� �� �ݺ�����")]
    public int repeatCount = 1;

    [Tooltip("�ݺ� ���� (��)")]
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
    /// i : �������� �ѹ�
    /// </summary>
    /// <param name="i"></param>
    void StartSpawn(int stageIndex)
    {
        if (stageIndex < 1 || stageIndex > 4)
            Debug.LogWarning("�������� �ε��� ����");
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
        timeline.spawnEvents.Sort((a,b) => a.spawnTime.CompareTo(b.spawnTime)); // �������� ���ķ� Ȥ�ø� �޸տ��� ���
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
        var go = PoolManager.instance.Get(monster);    // ������Ʈ Ǯ������ ���� Instantiate��� ���ٰ� �����ϸ� �ɰͰ����ϴ�
        Debug.Log($"�����Ŵ����� �������� {pos}");
        go.GetComponent<Monster>().Init(pos, dir, type); // ���� �ȿ��� OnEnable�� �ʱ�ȭ �� �� �ֱ� ������
                                                         // ������ġ, ��������� �����ϸ鼭 �� �� �־�� �پ��� ��η�
                                                         // ���͸� ���� �� ������ ���Ƽ� �����Ŵ������� ��ġ ���� ����

    }
}
