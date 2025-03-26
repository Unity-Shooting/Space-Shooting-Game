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
    }

    public override void Init(Vector2 pos, Vector2 dir, int type)
    {
        base.Init(pos, dir, type);
        if(type == 1)
        {
            MoveSpeed += 2;
        }
    }
}
