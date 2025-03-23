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
    private MonsterSupport caster; // 레이저 지속시간 중에 시전몬스터 사망 체크

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
        // 총알 범용 초기화 하고
        base.Init(pos, dir, 0);
        // 시전자 정보 받아오기
        this.caster = caster;


        StartCoroutine(LazerSequence());
    }

    IEnumerator LazerSequence()
    {
        float timer = 0f;

        //생성 시 콜라이더 꺼버리고 워닝 먼저 깔기 
        GetComponent<BoxCollider2D>().enabled = false;
        Warning.enabled = true;
        Ray.enabled = false;

        while (timer < warningTime)  // 경고표시 깜빡이는 부분
        {
            timer += Time.deltaTime;
            // 본체와 위치 동기화
            transform.position = caster.transform.position;

            if (caster == null || !caster.gameObject.activeInHierarchy)  // 시전자가 없거나 비활성화 되면 종료
            {
                if (caster == null)
                    Debug.Log("caster is null");
                Debug.Log("Ray is released");
                Release();
                yield break;
            }

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
