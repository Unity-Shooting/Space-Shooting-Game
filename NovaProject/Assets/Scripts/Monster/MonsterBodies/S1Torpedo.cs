using System.Collections;
using UnityEngine;

public class S1Torpedo : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    Coroutine firesequence;
    private Animator ani;

    protected override void StartAfterInit()
    {

        firesequence = StartCoroutine(FireSequence());
    }

    private void Awake()
    {
        ani = GetComponent<Animator>();
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

    IEnumerator Fire()
    {
        int bulletCount = 3; // 양쪽에서 쏘니까 3발
        float fireInterval = 0.3f;

        for (int i = 0; i < bulletCount; i++)
        {
            FireBullet(Launcher1.transform.position, Vector2.down, 0);
            FireBullet(Launcher2.transform.position, Vector2.down, 0);
            yield return new WaitForSeconds(fireInterval);
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
