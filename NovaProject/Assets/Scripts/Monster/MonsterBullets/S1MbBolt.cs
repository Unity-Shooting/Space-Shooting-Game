using UnityEngine;

public class S1MbBolt : MbBase
{

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // type이 1인 경우 더 빠른 총알
        if (type == 1)
            MoveSpeed += 1;
    }
}
