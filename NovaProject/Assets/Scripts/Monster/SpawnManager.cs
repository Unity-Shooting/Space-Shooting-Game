using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// ���� �� ��ü�� ���࿡ ���� ����, �ݺ��� ������!
/// </summary>
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

/// <summary>
/// �ϳ��� Wave�� ���� ����
/// SpawnTimelineSO �ȿ��� �������� SpawnEventData�� ����Ʈ�� �������
/// �� ������������ �����ϰ� startTime �Ŀ�, SpawnTileline�� ����ִ� ��ȯ�̺�Ʈ���� �ð� ������� ����
/// </summary>
[System.Serializable]
public class WaveData
{
    public int startTime;
    public SpawnTimelineSO timeline;
}

/// <summary>
/// ��ü������ ���� ������ ������
/// �̱������·� ���ӸŴ������� �� ���������� �����Ҷ�
/// StartStage(stageindex) ���ָ� ��!
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<StageWaveSO> stages = new();  // �ϳ��� ������������ ����� ���� WavaData�� ����Ʈ�� ����������

    void Start()
    {
        StartStage(1);   // �׽�Ʈ�� �ڵ�. 1�����̿� ����� ���� �ٷ� ����
    }
    /// <summary>
    /// �ش� ���������� ������ ����
    /// </summary>
    /// <param name="i"></param>
    void StartStage(int stageIndex)
    {
        if (stageIndex < 1 || stageIndex > stages.Count)
            Debug.LogWarning("�������� �ε��� ����");
        StartCoroutine(WaveStarter(stages[stageIndex - 1].waves));
    }

    /// <summary>
    /// �� ���������� �������� wave�� ����
    /// �� wave�� ������ startTime�� ������ �ش� wave�� ������
    /// </summary>
    /// <param name="waves"></param>
    /// <returns></returns>
    IEnumerator WaveStarter(List<WaveData> waves)
    {
        float currentTime = 0;  // �ð�üũ
        int i = 0;

        // �޸տ��� ������ ���� �ð� ������������ ��������
        waves.Sort((x, y) => x.startTime.CompareTo(y.startTime));


        while (i < waves.Count)
        {
            currentTime += Time.deltaTime; // ���� �ð� ���ϱ�

            var wave = waves[i];
            if (currentTime >= wave.startTime)
            {
                StartCoroutine(SpawnWave(wave.timeline)); // �ش� wave ���� ����!
                i++;
            }
            yield return null;
        }
    }

    IEnumerator SpawnWave(SpawnTimelineSO timeline)
    {
        timeline.spawnEvents.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime)); // �������� ���ķ� Ȥ�ø� �޸տ��� ���
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
    /// ����������, ������ġ, �ʱ��������, ���� �ൿŸ��
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// �� �Լ��� ���� ������ ������Ʈ Ǯ�� Ư���� �ѹ� ���� ������Ʈ�� ��ġ���� �̻��ϰ� �� �־
    /// �ݵ�� ��ġ���� �־�����ϴµ�(Instantiateó�� ������ �ʱⰪ���� �������� ����)
    /// ������ �ѹ��� ��԰� ���ҰͰ��Ƽ� �Լ��� ����׽��ϴ�
    private void SpawnMonster(GameObject monster, Vector3 pos, Vector2 dir, int type)
    {
        var go = PoolManager.instance.Get(monster);    // ������Ʈ Ǯ������ ���� Instantiate��� ���ٰ� �����ϸ� �ɰͰ����ϴ�
        Debug.Log($"�����Ŵ����� �������� {pos}");
        go.GetComponent<Monster>().Init(pos, dir, type); // ���� �ȿ��� OnEnable�� �ʱ�ȭ �� �� �ֱ� ������
                                                         // ������ġ, ��������� �����ϸ鼭 �� �� �־�� �پ��� ��η�
                                                         // ���͸� ���� �� ������ ���Ƽ� �����Ŵ������� ��ġ ���� ����

    }
}
