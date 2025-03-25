using System.Collections;
using UnityEngine;

public class MonsterSupport : Monster
{
    [SerializeField] public GameObject Launcher;
    /// <summary>
    /// �����ϴµ� �ɸ��� �ð�
    /// </summary>
    [SerializeField] private float stopDuration;
    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // ��� ����
        StartCoroutine(ShootAndReturn(type, stopDuration));  // type�� �� stopDuration���� ������ ����
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        //IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        //bullet.Init(Launcher.transform.position, Vector2.down, 0);
        // �� ���ʹ� �������� �߻��ϴµ� �������� �߰��� ���߱� ���ؼ� �μ��� �ϳ� �� �־����
        // IBulletInit�� ���� �������̽� ������ ���� ������ Ŭ������ ������
        MbRay ray = PoolManager.instance.Get(Bullet).GetComponent<MbRay>();
        ray.Init(Launcher.transform.position, direction, this);
    }

/// <summary>
/// Support���� ���ͼ� ��� ���ư���
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
            MoveSpeed = Mathf.Lerp(initMoveSpeed, 0f, time / duration); // ���� �ð��� ���� �ӵ��� �ʱ�ӵ�~0���� ����
            yield return null;
        }

        MoveSpeed = 0f; // �ð��� ���� �Ŀ� ������ �����ϵ��� ����

        // ������ ���ӽð� ���� ��� ��
        yield return new WaitForSeconds(Bullet.GetComponent<MbRay>().rayDuration-0.5f);

        time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            MoveSpeed = Mathf.Lerp(0f,initMoveSpeed, time / duration) * -1;
        }
        
    }
}
