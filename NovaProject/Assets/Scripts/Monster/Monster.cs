using UnityEngine;

// 모든 몬스터들이 상속 할 추상클래스
// 몬스터마다 이동이나 사격을 다르게 하려면 1945때랑 다르게 몬스터별로 클래스를 따로 써야 할 것 같아서
// 공통적으로 필요한 부분 
// 또한 어떤 몬스터를 만들더라도 스폰매니저에선 Monster.abc() 형태로 대부분의 기능을 사용 할 수 있도록 시도
public abstract class Monster : MonoBehaviour
{
    protected bool isReleased = false;  // 오브젝트 풀링 시 Release 함수를 호출해서 SetActive(false)로 비활성화 되면
                                        // OnBecameInvisible가 같이 호출돼는 문제를 해결하기 위한 트리거
                                        // OnDisable에서 OnBecameInvisible를 막는 방법이 있다고 하는데
                                        // 일단 지금 프로그램에선 Disalbe이 더 늦게 실행돼서 안될것같네요
                                        // OnEnable에서 false로 초기화, Release() 함수에서 true로 만들어서 OnBecameInvisible에서 Release를 막음
    public int HP { get; protected set; }  // 최대 체력이랑 구분해둔 이유 : 재생성 시 초기화 하기 위해
    public Vector2 direction { get; protected set; } // 초기 진행 방향

    [SerializeField]  // 인스펙터에서 조절할 수치들은 스크립트에서 초기값 안넣기(헷갈림)
    protected int MaxHP;
    [SerializeField]
    protected int Attack; // 공격력
    [SerializeField]
    protected float MoveSpeed; // 속도 

    // 상속받은 클래스에서 구현 할 메서드들
    public abstract void Shoot();
    public abstract void Init();
    // 스폰매니저에서 Get으로 오브젝트 가져온 다음에는 반드시 Init 해주기
    public abstract void Init(Vector2 a, Vector2 b);
    public abstract void Die();

    public virtual void Move(Vector2 dir) // dir 방향으로 speed 속도로 이동
    {
        transform.Translate(dir * MoveSpeed * Time.deltaTime);
    }

    public virtual void Damaged(int damage) // 데미지를 받을 때 체력 깎기
    {
        HP -= damage;
        Debug.Log($"{this.name} damaged : {damage} HP : {HP}");
        if (HP < 0)  // 체력 0이 되면 죽으면서 몬스터별 죽을 때 액션 수행
        {
            Debug.Log($"{this.name} destroyed");
            Die();
        }
    }
    protected virtual void OnEnable()  // 오브젝트풀에서 가져올 때 활성화(초기화)
    {
        isReleased = false;
        HP = MaxHP;
        this.gameObject.GetComponent<Animator>().SetBool("Destroyed", false);
    }

    protected void Release()
    {
        Debug.Log("Release 호출");
        isReleased = true;
        PoolManager.instance.Return(gameObject);
    }

}
