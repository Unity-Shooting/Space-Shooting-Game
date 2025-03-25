using UnityEngine;

/// <summary>
/// 탄환(Bullet)의 이동 및 속성을 제어하는 클래스. 
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 탄환의 이동 속도.
    /// </summary>
    public float speed = 3f;

    /// <summary>
    /// 탄환이 적중 시 입히는 피해량.
    /// </summary>
    public float damage = 3f;

    /// <summary>
    /// Rigidbody2D 컴포넌트 참조.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// 객체가 생성될 때 호출되며, Rigidbody2D를 초기화한다.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
    }

    /// <summary>
    /// 매 프레임마다 호출되며, 탄환을 전진시킨다.
    /// </summary>
    void Update()
    {
        // rb.MovePosition(rb.position + speed * Time.deltaTime * Vector2.up); // 너무 느려서 사용하지 않음
        transform.Translate(speed * Time.deltaTime * Vector2.up); // 탄환 이동
    }
}
