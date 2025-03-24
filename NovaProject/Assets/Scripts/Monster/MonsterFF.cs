using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class MonsterFF : Monster
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
        int count = 3; // 5�� �߻�
        float angle = 60f; // ��ä�� ����
        float intervalangle = angle / (count - 1); // �� źȯ ������ ����
        float baseangle = -angle / 2f; // ���� ���� źȯ�� ����


        for (int i = 0; i < count; i++)
        {
            float bulletangle = baseangle + intervalangle * i;

            Vector2 shootdir = Quaternion.Euler(0, 0, bulletangle) * direction;


            IBulletInit bullet1 = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
            bullet1.Init(Launcher.transform.position, shootdir, 0);


        }
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
