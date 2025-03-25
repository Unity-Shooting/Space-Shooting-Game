using System.Collections;
using UnityEngine;

public class MbBomb : MbBase
{
    [SerializeField] float explodetime; // 폭발 지연 시간
    [SerializeField] int bulletcount;  // 폭발 시 뿌릴 MbBullet 숫자
    [SerializeField] GameObject bullet;
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Explodeaftertime());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {   // bomb는 직접 충돌 없음
        return;
    }

    private void OnDisable()  // 혹시 터지기 전에 비활성화 되는 예외상황에 코루틴 종료
    {
        StopCoroutine(Explodeaftertime());
    }


    void Update()
    {
        Move();
    }

    IEnumerator Explodeaftertime()
    {
        yield return new WaitForSeconds(explodetime);
        Explode();
        Release();
    }

    private void Explode() // 터지면서 MbBullet 뿌리기
    {
        float angle = 360f;
        
        float angle2 = angle / (bulletcount - 1);
        for (int i = 0; i < bulletcount; i++)
        {
            float shootangle = angle2 * i;
            Vector2 shootdir = Quaternion.Euler(0, 0, shootangle) * direction;
            IBulletInit bul = PoolManager.instance.Get(bullet).GetComponent<IBulletInit>();
            bul.Init(transform.position, shootdir,0);
        }
    }
}
