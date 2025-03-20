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


    private void OnBecameInvisible() // ī�޶� ������ ������ ������Ʈ Ǯ�� ��ȯ
    {                               // �ٸ� �Լ����� ��ȯ�ص� �ش� �޼��尡 ����Ǳ� ������(��Ȱ��ȭ �Ǹ鼭 ī�޶󿡼� ������°����� �ν��ϴµ���)
                                    // Release()�� ȣ��Ǹ� isReleased�� True�� �ؼ� �ߺ������� ������
                                    // �̰� ���ϴϱ� ���� ������Ʈ�� �ι��� ��ȯ�ؼ� ������Ʈ Ǯ 10ĭ�� �� ���� ó������ ���ƿ���
                                    // ���� ������Ʈ�� �ι��� ȣ��ż� �ѹ��� �������� �ι辿 �������� ������ �߻�
                                    // �������°ͻӸ��� �ƴ϶� �� ������Ʈ�� �ι� �� �θ��� �̰����� ������ Ŀ�� �� �Ű����ҵ�!!
        Debug.Log("�κ�����");
        if (!isReleased)
        {
            Release();
        }
    }


}
