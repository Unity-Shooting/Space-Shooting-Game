using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class S1SC : Monster
{
    [SerializeField] private GameObject Launcher1;
    [SerializeField] private GameObject Launcher2;
    [SerializeField] private float curveRate;
    private Animator ani;
    Coroutine firesequence;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        direction = Vector2.down;
        MoveSpeed = 2f;
    }

    //protected override void StartAfterInit()
    //
    //    firesequence = StartCoroutine(FireSequence());

    //}
    void Update()
    {
        Turn();
        Move();
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
        Debug.Log("fired");
        Vector2 vectorToPlayer = PlayerController.Instance.transform.position - transform.position;

        FireBullet(Launcher2.transform.position, vectorToPlayer, 1);
        FireBullet(Launcher1.transform.position, vectorToPlayer, 1);
    }




    public override void Shoot()
    {
    }

    private void Turn()
    {
        float curve_direction = 1;

        if (type == 0) // 패턴 0이면 직선
        {
            return;
        }
        else if (type == 1) // 1이면 반시계방향 턴
        {
            curve_direction = 1;
        }
        else if (type == 2) // 2면 시계방향 턴
        {
            curve_direction = -1;
        }

        Vector2 tangent = new Vector2(-direction.y, direction.x); // 탄젠트벡터 (진행 방향의 왼쪽 수직)
        direction += curve_direction * curveRate * Time.deltaTime * tangent;  // 진행방향 벡터에 왼쪽방향 수직 벡터를 더해서 방향변경
        direction.Normalize();
        RotateToDirection(); // 바뀐 진행방향에 맞춰 스프라이트 회전
    }
}
