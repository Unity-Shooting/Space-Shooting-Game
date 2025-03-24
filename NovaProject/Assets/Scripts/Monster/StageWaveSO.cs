using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 스폰에 관한 정보를 리스트로 가지고 있는 오브젝트
/// 몬스터 한 웨이브가 어떻게 나타날지 인스펙터창에서 선택할수있게!
/// </summary>
[CreateAssetMenu(menuName = "Spawn/Stage")]
public class StageWaveSO : ScriptableObject
{
    public List<WaveData> waves = new();

}
