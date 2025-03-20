using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class MbBase : MonoBehaviour
{
    protected bool isReleased = false; // 중복반환 방지용 플래그
    public Vector2 direction { get; protected set; }
    public int Attack {  get; protected set; }
    public float MoveSpeed { get; protected set; }

    [SerializeField] protected int BaseAttack;
    [SerializeField] protected float BaseMoveSpeed;

    public virtual void Move()
    {
        transform.Translate(MoveSpeed * direction * Time.deltaTime);
    }

    protected virtual void OnEnable() // 초기화 Start() 대신 사용
    {
        isReleased = false;
        Attack = BaseAttack;
        MoveSpeed = BaseMoveSpeed;
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
        
    }
}
