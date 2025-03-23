using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ���� ������ ����Ʈ�� ������ �ִ� ������Ʈ
/// ���� �� ���̺갡 ��� ��Ÿ���� �ν�����â���� �����Ҽ��ְ�!
/// </summary>
[CreateAssetMenu(menuName = "Spawn/Timeline")]
public class SpawnTimelineSO : ScriptableObject
{
    [TextArea(2,5)]
    public string memo;
    public List<SpawnEventData> spawnEvents = new();
    
}
