using UnityEngine;

public class ContinuousBackGround : MonoBehaviour
{
    public float scrollSpeed = 0.5f;  // 이동 속도
    public float minY = -0.08f;       // 최소 위치
    public float maxY = 10f;          // 최대 위치
    private bool movingUp = true;     // 방향 확인

    void FixedUpdate()
    {
        MoveBackground();
    }

    void MoveBackground()
    {
        if (movingUp)
        {
            transform.position += Vector3.up * scrollSpeed * Time.fixedDeltaTime;
            if (transform.position.y >= maxY)
                movingUp = false;
        }
        else
        {
            transform.position -= Vector3.up * scrollSpeed * Time.fixedDeltaTime;
            if (transform.position.y <= minY)
                movingUp = true;
        }
    }
}