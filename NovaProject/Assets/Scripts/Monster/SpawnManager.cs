using UnityEngine;

[SerializeField]
public class SpawnEventData
{
    public float spawnTime;
    public GameObject monster;
    public Vector3 position;
    public Vector2 direction;
    public int type;
    [Header("นÝบน")]
    public int repeatCount = 1;
    public float repeatInterval = 1f;
}

[System.Serializable]
public class WaveData
{
    public int startTime;
    public SpawnTimelineSO timeline;

}

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
