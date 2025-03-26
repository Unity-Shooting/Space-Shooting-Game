using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class MonsterFF : Monster
{
    [SerializeField] private GameObject Launcher;
    /// <summary>
    /// 정지하는데 걸리는 시간
    /// </summary>
    [SerializeField] private float stopDuration;
    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // 사격 시작
        StartCoroutine(StopDuringDuration(type, stopDuration));  // type초 후 stopDuration동안 서서히 정지
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        int count = 3; // 5발 발사
        float angle = 60f; // 부채꼴 각도
        float intervalangle = angle / (count - 1); // 각 탄환 사이의 각도
        float baseangle = -angle / 2f; // 제일 왼쪽 탄환의 각도

         
        for (int i = 0; i < count; i++)
        {
            float bulletangle = baseangle + intervalangle * i;

            Vector2 shootdir = Quaternion.Euler(0, 0, bulletangle) * direction;


            IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet1.Init(Launcher.transform.position, shootdir, 0);


        }
    }

    /// <summary>
    /// delay초 후에 감속을 시작해 duration동안 멈추는 함수
    /// FF, Bomber, Torpedo, Support가 사용
    /// 위 4가지 유형의 몬스터는 생성할 때 type를 delay로 사용 ( 0이면 멈추지 않음)
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    protected IEnumerator StopDuringDuration(float delay, float duration)
    {
        // 몬스터 생성시 type를 정지까지 지연시간으로 사용할건데 0이면 정지하지 않는 패턴
        // 0이면 멈추지 않고 계속 가도록 코루틴 중지! 10이상으로 줄 일은 없을테니
        // 추가 패턴이 필요한 경우 11 등으로 줄 수 있게 10이상이어도 정지하지 않음
        if (delay == 0 || delay >= 10) yield break;

        yield return new WaitForSeconds(delay);
        float time = 0;
        float initMoveSpeed = MoveSpeed;
        while (time < duration)
        {
            time += Time.deltaTime;
            MoveSpeed = Mathf.Lerp(initMoveSpeed, 0f, time / duration); // 지난 시간에 따라 속도를 초기속도~0으로 보간
            yield return null;
        }

        MoveSpeed = 0f; // 시간이 지난 후에 완전히 정지하도록 보장
    }
}
