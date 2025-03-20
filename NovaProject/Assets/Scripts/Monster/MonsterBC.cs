using System.Collections;
using UnityEngine;

public class MonsterBC : Monster
{
    protected override void OnEnable()  // Start()�� ���� ���� �Ѵٰ� ���ø� �˴ϴ�
    {
        base.OnEnable();    // ��ӹ��� Monster Ŭ������ OnEnable() ����. �������� ������ �ʱ�ȭ�� ���⼭ ����
        StartCoroutine(Death()); // �ı� �ִϸ��̼� �׽�Ʈ��
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {

    }

    IEnumerator Death()  // �ı� �ִϸ��̼� �׽�Ʈ�� �ڷ�ƾ
    {
        yield return new WaitForSeconds(0.1f);
        Die();
        Debug.Log("Death");
    }

    private void OnDisable() // �ı��� �� �����ؾ��� �� ������ ���⿡�� �ϱ�. ������ �Ƚ����� Invoke�� ���ԵǸ� ���⼭ ĵ�����ٰ�
    {
    }





}
