using System.Collections;
using UnityEngine;

public class MbBomb : MbBase
{
    [SerializeField] float explodetime; // ���� ���� �ð�
    [SerializeField] int bulletcount;  // ���� �� �Ѹ� MbBullet ����
    [SerializeField] GameObject bullet;
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Explodeaftertime());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {   // bomb�� ���� �浹 ����
        return;
    }

    private void OnDisable()  // Ȥ�� ������ ���� ��Ȱ��ȭ �Ǵ� ���ܻ�Ȳ�� �ڷ�ƾ ����
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

    private void Explode() // �����鼭 MbBullet �Ѹ���
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
