using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ���� ������ ����Ʈ�� ������ �ִ� ������Ʈ
/// ���� �� ���̺갡 ��� ��Ÿ���� �ν�����â���� �����Ҽ��ְ�!
/// </summary>
[CreateAssetMenu(menuName = "Spawn/Stage")]
public class StageWaveSO : ScriptableObject
{
    public List<WaveData> waves = new();

}
