using System.Collections;
using UnityEngine;

public class MbRay : MbBase
{
    [SerializeField] private float warningTime;
    [SerializeField] private float rayDuration;
    [SerializeField] private SpriteRenderer Warning;
    [SerializeField] private SpriteRenderer Ray;
    [SerializeField] private GameObject Circle;

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //���� �� �ݶ��̴� �������� ���� ���� ��� 
        GetComponent<BoxCollider2D>().enabled = false;
        Warning.enabled = true;
        Ray.enabled = false;

        StartCoroutine(LazerSequence());

    }

    IEnumerator LazerSequence()
    {
        //yield return new WaitForSeconds(warningTime);  // ���ð���ŭ ��� ��
        float timer = 0f;
        while (timer < warningTime)  // ���ǥ�� �����̴� �κ�
        {
            timer += Time.deltaTime;

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
