using System.Collections;
using UnityEngine;

public class MonsterBomber : Monster
{
    [SerializeField] private GameObject Launcher;
    /// <summary>
    /// 정지하는데 걸리는 시간
    /// </summary>
    [SerializeField] private float stopDuration;
    protected override void OnEnable()  // Start()랑 같은 역할 한다고 보시면 됩니다
    {
        base.OnEnable();    // 상속받은 Monster 클래스의 OnEnable() 실행. 공통적인 변수들 초기화는 저기서 했음

        InvokeRepeating("Shoot", AttackStart, AttackSpeed);
        StartCoroutine(StopDuringDuration(type, stopDuration));
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet.Init(Launcher.transform.position, direction, 0);
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
        Debug.Log($"delay : {delay}");
        // 몬스터 생성시 type를 정지까지 지연시간으로 사용할건데 0이면 정지하지 않는 패턴
        // 0이면 멈추지 않고 계속 가도록 코루틴 중지! 10이상으로 줄 일은 없을테니
        // 추가 패턴이 필요한 경우 11 등으로 줄 수 있게 10이상이어도 정지하지 않음
        if (delay == 0 || delay >= 10)
        {
            yield break;
        }

        Debug.Log("Enterd Stop");

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
