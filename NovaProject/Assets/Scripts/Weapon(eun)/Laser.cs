using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform firePoint1; // 첫 번째 발사 지점
    public Transform firePoint2; // 두 번째 발사 지점
    private string laserIdentifier; // 레이저 식별자

    void Start()
    {

    }

    void Update()
    {
        // 레이저 식별자에 따라 발사 지점을 따라감
        if (laserIdentifier.Contains("laser1"))
        {
            transform.position = firePoint1.position + (Vector3.up * 5f);
        }
        else if (laserIdentifier.Contains("laser2"))
        {
            transform.position = firePoint2.position + (Vector3.up * 5f);
        }
    }

    // 레이저 식별자를 외부에서 설정하는 메서드
    public void SetLaserIdentifier(string identifier)
    {
        laserIdentifier = identifier;
    }
}
