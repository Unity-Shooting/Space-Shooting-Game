using System.Collections;
using UnityEngine;

public class MonsterBomber : Monster
{
    [SerializeField] private GameObject Launcher;
    /// <summary>
    /// �����ϴµ� �ɸ��� �ð�
    /// </summary>
    [SerializeField] private float stopDuration;
    protected override void OnEnable()  // Start()�� ���� ���� �Ѵٰ� ���ø� �˴ϴ�
    {
        base.OnEnable();    // ��ӹ��� Monster Ŭ������ OnEnable() ����. �������� ������ �ʱ�ȭ�� ���⼭ ����

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
    /// delay�� �Ŀ� ������ ������ duration���� ���ߴ� �Լ�
    /// FF, Bomber, Torpedo, Support�� ���
    /// �� 4���� ������ ���ʹ� ������ �� type�� delay�� ��� ( 0�̸� ������ ����)
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    protected IEnumerator StopDuringDuration(float delay, float duration)
    {
        Debug.Log($"delay : {delay}");
        // ���� ������ type�� �������� �����ð����� ����Ұǵ� 0�̸� �������� �ʴ� ����
        // 0�̸� ������ �ʰ� ��� ������ �ڷ�ƾ ����! 10�̻����� �� ���� �����״�
        // �߰� ������ �ʿ��� ��� 11 ������ �� �� �ְ� 10�̻��̾ �������� ����
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
            MoveSpeed = Mathf.Lerp(initMoveSpeed, 0f, time / duration); // ���� �ð��� ���� �ӵ��� �ʱ�ӵ�~0���� ����
            yield return null;
        }

        MoveSpeed = 0f; // �ð��� ���� �Ŀ� ������ �����ϵ��� ����
    }
}
