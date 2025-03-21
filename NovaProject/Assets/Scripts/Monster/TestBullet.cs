using UnityEngine;

public class TestBullet : MbBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        direction = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    
}
