using UnityEngine;

public class ArrowWarning : MonoBehaviour
{
    private Vector2 direction; // 경고표시의 방향
    bool isReleased = false; // release() 됐는지 체크
    private float BaseMaskY = 5.57f; // 스프라이트 마스크 초기 위치
    private float EndMaskY = -13.5f; // 스프라이트 마스크 최종 위치
    private int warningCount = 0; // 경고 화살표 표시 횟수
    private float warningTimer; // 경고 화살표 표시 타이머
    [SerializeField] private GameObject mask; // 마스크 오브젝트
    [SerializeField] private float warningTime; // 경고 화살표 1회 도착 시간


    private void StartAfterInit()
    {


    }

    public void Init(Vector2 pos, Vector2 dir)
    {
        transform.position = pos;
        direction = dir;
        RotateToDirection(direction);
        StartAfterInit();
        warningCount = 0;
        warningTimer = 0;
    }




    void RotateToDirection(Vector2 dir) // dir 방향에 맞춰 회전
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }


    // Update is called once per frame
    void Update()
    {
        warningTimer += Time.deltaTime;

        if (warningTimer >= warningTime)
        {
            warningTimer = 0;
            warningCount++;
            if (warningCount >= 3)  // warningTime 한번 더 기다린 후에 빨간바닥 마저 사라짐
            {
                //Release();
                Destroy(gameObject);
            }
        }

        if (warningCount <3) // 화살표 움직이는거 3번 보여주고 4번째에는 화살표는 없이 빨간바닥만 보여줌
        {
            // SpriteMask로 화살표 보여주기
            // 화살표는 사실 처음부터 끝까지 존재하는데, 마스크를 이용해 보여주는 부분을 조절함
            // 마스크가 있는 부분만 화살표가 보이게 됨
            // 직사각형의 마스크가 시작점부터 끝까지 움직이면서 겹치는 부분만 화살표가 보이게 돼서
            // 화살표가 움직이는듯한 효과를 주게 됨
            float maskY = Mathf.Lerp(BaseMaskY, EndMaskY, warningTimer / warningTime);
            mask.transform.localPosition = new Vector3(0, maskY, 0);
        }
    }

    protected void Release()  // 오브젝트 풀로 반환 
    {

        if (!isReleased) // 중복리턴 방지
        {
            isReleased = true;
            PoolManager.instance.Return(gameObject);
        }
    }
}
