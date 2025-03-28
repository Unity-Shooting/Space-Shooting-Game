using System.Collections;
using UnityEngine;

public class S1Support : Monster
{
    [SerializeField] public GameObject Launcher;
    /// <summary>
    /// 정지하는데 걸리는 시간
    /// </summary>
    [SerializeField] public float stopDuration;
    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // 사격 시작
        StartCoroutine(ShootAndReturn(type, stopDuration));  // type초 후 stopDuration동안 서서히 정지
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        //IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        //bullet.Init(Launcher.transform.position, Vector2.down, 0);
        // 이 몬스터는 레이저를 발사하는데 레이저를 중간에 멈추기 위해서 인수를 하나 더 넣어야함
        // IBulletInit로 범용 인터페이스 못쓰고 따로 레이저 클래스로 가져옴
        S1MbRay ray = PoolManager.instance.Get(Bullet).GetComponent<S1MbRay>();
        ray.Init(Launcher.transform.position, direction, this);
    }

/// <summary>
/// Support전용 나와서 쏘고 돌아가기
/// </summary>
/// <param name="delay"></param>
/// <param name="duration"></param>
/// <returns></returns>
    protected IEnumerator ShootAndReturn(float delay, float duration)
    {
        

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

        // 레이저 지속시간 동안 대기 후
        yield return new WaitForSeconds(Bullet.GetComponent<MbRay>().rayDuration);

        time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            MoveSpeed = Mathf.Lerp(0f,initMoveSpeed, time / duration) * -1;
            yield return null;
        }
        
    }
}
