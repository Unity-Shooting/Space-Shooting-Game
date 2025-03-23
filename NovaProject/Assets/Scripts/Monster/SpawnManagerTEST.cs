using System.Collections;
using UnityEngine;


// �� ���� ����
public class SpawnManagerTest : MonoBehaviour
{
    [SerializeField]
    private float spawntimer;

    [SerializeField]
    private GameObject monsterA;

    

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() // ������ �� �ƴ��� �׽�Ʈ�� ����
    {

        SpawnMonster(monsterA, new Vector3(-0.3f, 2.0f, 0), Vector2.right,1);
        while (true) // ����Ÿ�̸� �������� monsterA ��ȯ
        {
            SpawnMonster(monsterA,transform.position,Vector2.down,2);
            yield return new WaitForSeconds(spawntimer);
            Debug.Log(transform.position);
        }
    }
    /// <summary>
    /// ����������, ������ġ, �ʱ��������
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// �� �Լ��� ���� ������ ������Ʈ Ǯ�� Ư���� �ѹ� ���� ������Ʈ�� ��ġ���� �̻��ϰ� �� �־
    /// �ݵ�� ��ġ���� �־�����ϴµ�(Instantiateó�� ������ �ʱⰪ���� �������� ����)
    /// ������ �ѹ��� ��԰� ���ҰͰ��Ƽ� �Լ��� ����׽��ϴ�
    private void SpawnMonster(GameObject monster, Vector3 pos, Vector2 dir,int type) 
    {
        var go = PoolManager.instance.Get(monster);    // ������Ʈ Ǯ������ ���� Instantiate��� ���ٰ� �����ϸ� �ɰͰ����ϴ�
        Debug.Log($"�����Ŵ����� �������� {pos}");
        go.GetComponent<Monster>().Init(pos, dir,type); // ���� �ȿ��� OnEnable�� �ʱ�ȭ �� �� �ֱ� ������
                                                                           // ������ġ, ��������� �����ϸ鼭 �� �� �־�� �پ��� ��η�
                                                                           // ���͸� ���� �� ������ ���Ƽ� �����Ŵ������� ��ġ ���� ����

    }
}
