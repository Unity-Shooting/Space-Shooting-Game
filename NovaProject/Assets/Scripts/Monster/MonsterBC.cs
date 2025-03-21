using System.Collections;
using UnityEngine;

public class MonsterBC : Monster
{
    protected override void OnEnable()  // Start()�� ���� ���� �Ѵٰ� ���ø� �˴ϴ�
    {
        base.OnEnable();    // ��ӹ��� Monster Ŭ������ OnEnable() ����. �������� ������ �ʱ�ȭ�� ���⼭ ����
                            //StartCoroutine(Death()); // �ı� �ִϸ��̼� �׽�Ʈ��

        InvokeRepeating("Shoot", 1, 1);
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()  
    {
        MbBullet bul = PoolManager.instance.Get(Bullet).GetComponent<MbBullet>();
        bul.Init(transform.position, Vector2.down);
    }

    IEnumerator Death()  // �ı� �ִϸ��̼� �׽�Ʈ�� �ڷ�ƾ
    {
        yield return new WaitForSeconds(0.1f);
        Die();
        Debug.Log("Death");
    }






}
