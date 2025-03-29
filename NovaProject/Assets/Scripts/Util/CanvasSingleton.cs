using UnityEngine;

public class CanvasSingleton : Singleton<CanvasSingleton>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
