using UnityEngine;

public class MonsterA : Monster
{
    private Vector2 direction;
    void Start()
    {
        direction = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);
    }

    public override void Shoot()
    {

    }

    public override void Move(Vector2 dir)
    {
        transform.Translate(dir * MoveSpeed * Time.deltaTime);
    }
}
