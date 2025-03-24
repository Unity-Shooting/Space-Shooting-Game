using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class MbBase : MonoBehaviour, IBulletInit
{
    protected bool isReleased = false; // 중복반환 방지용 플래그
    public Vector2 direction { get; protected set; }
    public int Attack { get; protected set; }
    public float MoveSpeed { get; protected set; }

    [SerializeField] protected int BaseAttack;
    [SerializeField] protected float BaseMoveSpeed;

    public virtual void Init(Vector2 pos, Vector2 dir, int type)
    {
        transform.position = pos;
        direction = dir.normalized;

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

    protected virtual void OnTriggerEnter2D(Collider2D collision) // 플레이어 충돌 시 액션
    {
        ////테스트용으로 몬스터랑 충돌
        //if (collision.gameObject.CompareTag("Monster"))
        //{
        //    IDamageable obj = collision.GetComponent<IDamageable>();
        //    obj.TakeDamage(1000);
        //    Release();
        //}

        if(collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(Attack);
            Release();
        }

    }

    protected virtual void OnBecameInvisible()
    {
        Release();
    }

    protected void RotateToDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // 이미지가 아래방향이니까 90도 보정
    }
}