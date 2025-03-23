using UnityEngine;

public class BaseSingleton : Singleton<BaseSingleton>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
