using System.Collections;
using System.IO.Pipes;
using UnityEngine;

public class S1FF : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    Coroutine firesequence;
    private Animator ani;

    //protected override void StartAfterInit()
    //{

    //    firesequence = StartCoroutine(FireSequence());
    //}

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Start()  // 프리펩화 하기 전 하이어러키창에서 임시 테스트용
    {
        firesequence = StartCoroutine(FireSequence());
    }

    IEnumerator FireSequence()
    {
        yield return new WaitForSeconds(AttackStart);

        while (true)
        {
            ani.SetTrigger(nameof(Fire));
            Debug.Log($"Trigger has set");
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    public void Fire()
    {
        int bulletCount = 3; // 양쪽에서 쏴서 6발
        float spreadAngle = 20f;
        Vector2 vectorToPlayer = PlayerController.Instance.transform.position - transform.position;
        Vector2 firedirection;

        //왼쪽 세발
        for (int i = 0; i < bulletCount; i++)
        {
            firedirection = Quaternion.Euler(0, 0, (-spreadAngle + spreadAngle*i) ) * vectorToPlayer;
            FireBullet(Launcher2.transform.position, firedirection, 0);
        }
        // 오른쪽 세발
        for (int i = 0; i < bulletCount; i++)
        {
            firedirection = Quaternion.Euler(0, 0, (-spreadAngle + spreadAngle * i)) * vectorToPlayer;
            FireBullet(Launcher1.transform.position, firedirection, 0);
        }


    }



    protected override void OnDisable()
    {
        base.OnDisable();
        if (firesequence != null)
            StopCoroutine(firesequence);
    }


    void Update()
    {
        Move();
    }

    public override void Shoot()
    {

    }
}
