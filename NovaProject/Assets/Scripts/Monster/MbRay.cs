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

        //생성 시 콜라이더 꺼버리고 워닝 먼저 깔기 
        GetComponent<BoxCollider2D>().enabled = false;
        Warning.enabled = true;
        Ray.enabled = false;

        StartCoroutine(LazerSequence());

    }

    IEnumerator LazerSequence()
    {
        //yield return new WaitForSeconds(warningTime);  // 경고시간만큼 대기 후
        float timer = 0f;
        while (timer < warningTime)  // 경고표시 깜빡이는 부분
        {
            timer += Time.deltaTime;

            //경고 깜빡이기
            float alpha = Mathf.PingPong(Time.time * 2f, 1f);
            Color c = Warning.color;
            c.a = alpha;
            Warning.color = c;

            //몬스터 앞에 원 커지기
            float scale = Mathf.Lerp(0, 0.3f, timer / warningTime);
            Circle.transform.localScale = new Vector3(scale, scale, 1);

            yield return null; // 프레임단위로 대기?
        }
        Warning.enabled = false;  // 경고 끄고 레이저 켜기
        Ray.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true; // 콜라이더 활성화

        yield return new WaitForSeconds(rayDuration);

        Release();

    }


}
