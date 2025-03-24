using System.Collections;
using UnityEngine;

public class MonsterTorpedo : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    /// <summary>
    /// �����ϴµ� �ɸ��� �ð�
    /// </summary>
    [SerializeField] private float stopDuration;

    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // ��� ����
        StartCoroutine(StopDuringDuration(type, stopDuration));  // type�� �� stopDuration���� ������ ����
    }

    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet1.Init(Launcher1.transform.position, direction, 0);
        IBulletInit bullet2 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet2.Init(Launcher2.transform.position, direction, 0);
    }


/// <summary>
/// delay�� �Ŀ� ������ ������ duration���� ���ߴ� �Լ�
/// FF, Bomber, Torpedo, Support�� ���
/// �� 4���� ������ ���ʹ� ������ �� type�� delay�� ��� ( 0�̸� ������ ����)
/// </summary>
/// <param name="delay"></param>
/// <param name="duration"></param>
/// <returns></returns>
    protected IEnumerator StopDuringDuration(float delay, float duration)
    {
        // ���� ������ type�� �������� �����ð����� ����Ұǵ� 0�̸� �������� �ʴ� ����
        // 0�̸� ������ �ʰ� ��� ������ �ڷ�ƾ ����! 10�̻����� �� ���� �����״�
        // �߰� ������ �ʿ��� ��� 11 ������ �� �� �ְ� 10�̻��̾ �������� ����
        if (delay == 0 || delay >= 10) yield break; 

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
    }
}
