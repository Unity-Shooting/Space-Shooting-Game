using UnityEngine;
using System.Collections;

public class Backgroundairplane : MonoBehaviour
{
    [Header("이동 관련")]
    public float moveSpeed = 8f;         // 이동 속도
    public float waitTime = 10f;          // 다음 이동까지 대기 시간

    public float startX = 6f;          // 출발 위치 (오른쪽 밖)
    public float endX = -11f;             // 도착 위치 (왼쪽 밖)

    [Header("랜덤 높이 범위")]
    public float minY = -4f;              // 최소 높이
    public float maxY = 4f;         // 최대 높이

    [Header("흔들림 연출")]
    public float wobbleAmount = 0.2f;    // 상하 흔들림 범위
    public float wobbleSpeed = 4f;       // 흔들림 속도

    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            // 랜덤한 높이 선택
            float randomY = Random.Range(minY, maxY);

            // 시작과 끝 위치 설정 (Y는 랜덤, 방향은 고정 오→왼)
            startPos = new Vector3(startX, randomY, 0);
            endPos = new Vector3(endX, randomY, 0);

            // 비행기 위치를 시작 지점으로 이동
            transform.position = startPos;

            float t = 0f;
            float totalDistance = Vector3.Distance(startPos, endPos);

            // 이동 루프
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed / totalDistance;

                // 흔들림 계산
                float wobble = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

                // 보간 위치 계산 후 흔들림 반영
                Vector3 basePos = Vector3.Lerp(startPos, endPos, t);
                transform.position = new Vector3(basePos.x, basePos.y + wobble, basePos.z);

                yield return null;
            }

            // 다음 등장까지 대기
            yield return new WaitForSeconds(waitTime);
        }
    }

}
