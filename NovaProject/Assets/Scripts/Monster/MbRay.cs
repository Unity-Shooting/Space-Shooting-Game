using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MbRay : MbBase
{
    [SerializeField] private float warningTime;
    [SerializeField] private float rayDuration;
    [SerializeField] private SpriteRenderer Warning;
    [SerializeField] private SpriteRenderer Ray;
    [SerializeField] private GameObject Circle;
    private MonsterSupport caster; // ������ ���ӽð� �߿� �������� ��� üũ

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        caster = null;
    }

    public void Init(Vector2 pos, Vector2 dir, MonsterSupport caster)
    {
        // �Ѿ� ���� �ʱ�ȭ �ϰ�
        base.Init(pos, dir, 0);
        // ������ ���� �޾ƿ���
        this.caster = caster;


        StartCoroutine(LazerSequence());
    }

    IEnumerator LazerSequence()
    {
        float timer = 0f;

        //���� �� �ݶ��̴� �������� ���� ���� ��� 
        GetComponent<BoxCollider2D>().enabled = false;
        Warning.enabled = true;
        Ray.enabled = false;

        while (timer < warningTime)  // ���ǥ�� �����̴� �κ�
        {
            timer += Time.deltaTime;
            // ��ü�� ��ġ ����ȭ
            transform.position = caster.transform.position;

            if (caster == null || !caster.gameObject.activeInHierarchy)  // �����ڰ� ���ų� ��Ȱ��ȭ �Ǹ� ����
            {
                if (caster == null)
                    Debug.Log("caster is null");
                Debug.Log("Ray is released");
                Release();
                yield break;
            }

            //��� �����̱�
            float alpha = Mathf.PingPong(Time.time * 2f, 1f);
            Color c = Warning.color;
            c.a = alpha;
            Warning.color = c;

            //���� �տ� �� Ŀ����
            float scale = Mathf.Lerp(0, 0.3f, timer / warningTime);
            Circle.transform.localScale = new Vector3(scale, scale, 1);

            yield return null; // �����Ӵ����� ���?
        }
        Warning.enabled = false;  // ��� ���� ������ �ѱ�
        Ray.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true; // �ݶ��̴� Ȱ��ȭ

        yield return new WaitForSeconds(rayDuration);

        Release();

    }


}
