using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class MbBase : MonoBehaviour, IBulletInit
{
    protected bool isReleased = false; // 중복반환 방지용 플래그
    public Vector2 direction { get; protected set; }
    public int Attack { get; protected set; }
    public float MoveSpeed { get; protected set; }

    private float relaseTimerInvisible = 2f;
    Coroutine releaseTimer;

    [SerializeField] protected int BaseAttack;
    [SerializeField] protected float BaseMoveSpeed;
    protected int type;

    public virtual void Init(Vector2 pos, Vector2 dir, int type)
    {
        transform.position = pos;
        direction = dir.normalized;
        this.type = type;
        RotateToDirection();
    }
    public virtual void Move()
    {
        transform.Translate(MoveSpeed * direction * Time.deltaTime, Space.World);  // 이동을 월드좌표 기준으로해서 rotation 영향 안받기
    }

    protected virtual void OnEnable() // 초기화 Start() 대신 사용
    {
        transform.rotation = Quaternion.identity;
        isReleased = false;
        Attack = BaseAttack;
        MoveSpeed = BaseMoveSpeed;
        direction = Vector2.zero;
    }

    protected virtual void Release() // 오브젝트 풀로 리턴 
    {
        if (!isReleased)
        {
            isReleased = true;
            PoolManager.instance.Return(gameObject);
        }
    }

    /// <summary>
    /// 시야에서 나갔을 때 호출, 일정시간 뒤에 Release()를 호출
    /// </summary>
    /// <returns></returns>
    IEnumerator RelaseAfterTimer()
    {
        yield return new WaitForSeconds(relaseTimerInvisible);
        Release();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) // 플레이어 충돌 시 액션
    {

        if(collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(Attack);
            Release();
        }

    }

    /// <summary>
    /// 시야 밖으로 나가면 2초 뒤에 release하는 코루틴 호출
    /// </summary>
    protected virtual void OnBecameInvisible()
    {
        releaseTimer = StartCoroutine(RelaseAfterTimer());
    }

    /// <summary>
    /// 다시 시야에 들어오면 2초 뒤에 release하는 코루틴 중지
    /// </summary>
    protected virtual void OnBecameVisible()
    {
        if (releaseTimer != null)
        {
            StopCoroutine(releaseTimer);
        }
    }

    protected void RotateToDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // 이미지가 아래방향이니까 90도 보정
    }
}