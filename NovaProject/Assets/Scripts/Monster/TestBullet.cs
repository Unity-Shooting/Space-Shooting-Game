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


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("충돌 발생");
            var i = collision.gameObject.GetComponent<IDamageable>();
            i.TakeDamage(100);
        }
    }

}
