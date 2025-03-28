using System.Collections;
using UnityEngine;

// BackgroundPlanet 클래스: 배경 속 행성이 아래로 내려가면서 회전하고, 화면 밖으로 나가면 다시 위로 재배치되는 기능
public class BackgroundPlanet : MonoBehaviour
{
    // 행성이 아래로 내려가는 속도
    public float scrollSpeed = 2f;

     // 리스폰될 때 적용할 최소/최대 스케일 범위 (행성 크기 랜덤화)
    public float minScale = 0.8f;
    public float maxScale = 1.5f;

    // 리스폰될 때 적용할 X 좌표 범위 (좌우 위치 랜덤화)
    public float minX = -4f;
    public float maxX = 4f;

    [Header("회전 속도의 범위 설정")]
    // 회전 속도의 범위 (리스폰 시 -rotationSpeed ~ +rotationSpeed 사이 랜덤)
    public float rotationSpeed = 30f;
     // 현재 적용 중인 회전 속도 (매 프레임마다 사용됨)
    private float currentRotationSpeed = 30f;

    // 리스폰될 때 사용할 랜덤 X 좌표 값, 랜덤 스케일값 저장
    private float randomX = 5;
    private float randomScale;

    // 매 프레임 호출: 행성을 아래로 이동시키고 회전시킴
    private void Update()
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);
    }

    // 카메라에서 사라졌을 때 실행
    private void OnBecameInvisible()
    {
        RespawnAtTop();  // 오브젝트를 화면 위쪽으로 재배치
        SetRandomDirectionAndRotation(); // 회전 속도와 위치, 크기를 랜덤으로 다시 설정
    }

    // 행성을 화면 위쪽으로 이동시키는 함수
    private void RespawnAtTop()
    {
        // Y 위치를 화면 위쪽으로 이동 (카메라 높이 × 2배)
        transform.position = new Vector3(randomX, 6f, transform.position.z);
        // 랜덤 크기를 적용 (X, Y 동일한 비율로 스케일 조정)
        transform.localScale = new Vector3(randomScale, randomScale, 1f);       
    }

    // 회전 속도, 스케일, 위치, 초기 회전 각도를 랜덤하게 설정하는 함수
    private void SetRandomDirectionAndRotation()
    {
        // 좌우 위치를 랜덤으로 설정
        randomX = Random.Range(minX, maxX);
        // 크기를 랜덤으로 설정
        randomScale = Random.Range(minScale, maxScale);
        // 시작 회전 각도를 랜덤으로 설정 (0 ~ 360도)
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        // 회전 속도를 랜덤으로 설정 Ex) -30 ~ +30
        currentRotationSpeed = Random.Range(-rotationSpeed, rotationSpeed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) //충돌처리
    {

        if(collision.CompareTag("Player")) //Player tag 발견
        {
            PlayerHealth.Instance.TakeDamage(1); //데미지 1
        }

    }
}
