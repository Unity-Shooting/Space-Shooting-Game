using UnityEngine;

// 모든 몬스터들이 상속 할 추상클래스
// 몬스터마다 이동이나 사격을 다르게 하려면 1945때랑 다르게 몬스터별로 클래스를 따로 써야 할 것 같아서
// 공통적으로 필요한 부분 
// 또한 어떤 몬스터를 만들더라도 스폰매니저에선 Monster.abc() 형태로 대부분의 기능을 사용 할 수 있도록 시도

/// <summary>
/// 각 몬스터가 각자 가져야 할 프로퍼티
/// Gameobject Launcher
/// </summary>
public abstract class Monster : MonoBehaviour, IDamageable
{
    protected bool isReleased = false;  // 오브젝트 풀링 시 Release 함수를 호출해서 SetActive(false)로 비활성화 되면
                                        // OnBecameInvisible가 같이 호출돼는 문제를 해결하기 위한 트리거
                                        // OnDisable에서 OnBecameInvisible를 막는 방법이 있다고 하는데
                                        // 일단 지금 프로그램에선 Disalbe이 더 늦게 실행돼서 안될것같네요
                                        // OnEnable에서 false로 초기화, Release() 함수에서 true로 만들어서 OnBecameInvisible에서 Release를 막음
    public int HP { get; protected set; }  // 초기값이랑 구분해둔 이유 : 재생성 시 초기화 하기 위해
    public int Attack { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttackStart { get; protected set; }
    public Vector2 direction { get; protected set; } // 진행 방향
    public int type { get; protected set; }
    // 인스펙터에서 조절할 수치들은 스크립트에서 초기값 안넣기(헷갈림)
    [SerializeField] protected int BaseHP;
    [SerializeField] protected int BaseAttack; // 공격력
    [SerializeField] protected float BaseMoveSpeed; // 속도 
    [SerializeField] protected float BaseAttackSpeed;
    [SerializeField] protected float BaseAttackStart; // 첫 공격까지 지연시간
    [SerializeField] protected GameObject Bullet; // 발사할 총알 프리펩
    [SerializeField] protected int Score; // 발사할 총알 프리펩



    // 상속받은 클래스에서 구현 할 메서드들

    // 보스가 아닌 몬스터들은 모두 일정한 간격으로 사격을 실행할 예정이기떄문에
    // 코루틴보다 인보크리피팅을 사용해서 Shoot을 불러올 예정
    // 따라서 OnDisable()에서 CancleInvoke 해줌
    public abstract void Shoot();


    public virtual void Die()
    {
        CancelInvoke("Shooting");
        this.gameObject.GetComponent<Animator>().SetTrigger("Destroy");  // 파괴 애니메이션 재생
                                                                         // 애니매이션 종료시 Release() 호출하도록 애니메이션 클립 설정해둠
        DestroyAllChildren();  // 파괴애니메이션 시작하면서 실드, 엔진 등 자식오브젝트 꺼주기

    }

    // 스폰매니저에서 Get으로 오브젝트 가져온 다음에는 반드시 Init 해주기
    public virtual void Init(Vector3 pos, Vector2 dir, int type)
    {
        Debug.Log($"Init의 pos {pos}");
        transform.position = pos;
        Debug.Log($"Init의 transform.position {transform.position}");
        direction = dir.normalized;
        this.type = type;

        // 방향벡터에 맞춰서 이미지 회전
        RotateToDirection();
    }
    public virtual void Move() // dir 방향으로 speed 속도로 이동
    {
        transform.Translate(MoveSpeed * Time.deltaTime * direction, Space.World);
    }

    public virtual void TakeDamage(int damage) // 데미지를 받을 때 체력 깎기
    {
        HP -= damage;
        Debug.Log($"{this.name} damaged : {damage} HP : {HP}");
        if (HP <= 0)  // 체력 0이 되면 죽으면서 몬스터별 죽을 때 액션 수행
        {
            Debug.Log($"{this.name} destroyed");
            Die();
        }
    }
    protected virtual void OnEnable()  // 오브젝트풀에서 가져올 때 활성화(초기화)
    {
        transform.rotation = Quaternion.identity;
        isReleased = false;
        HP = BaseHP;
        Attack = BaseAttack;
        MoveSpeed = BaseMoveSpeed;
        AttackSpeed = BaseAttackSpeed;
        AttackStart = BaseAttackStart;
        type = 0;
    }

    protected virtual void OnDisable()  // 대부분의 몬스터는 Shoot()를 InvokeRepeating 할 예정이기때문에 비활성화시 취소
    {                                   // 만약 인보크 하지 않는 경우여도 성능상 크게 문제가 없다고 하니 부모클래스에서 일괄실행
        CancelInvoke("Shoot");
    }

    /// <summary>
    /// Destroy() 대신 사용합니다. 오브젝트 풀로 리턴
    /// </summary>
    protected void Release()
    {

        if (!isReleased) // 중복리턴 방지
        {
            isReleased = true;
            PoolManager.instance.Return(gameObject);
        }
    }

    /// <summary>
    /// transform 이 자식 오브젝트들을 보관하고 있어서 이런 foreach로 자식을 한바퀴 돌 수 있음
    /// 파괴 애니메이션이 시작되면서 Engine,Shield 등을 꺼주기</summary>
    private void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false); // 자식 오브젝트 비활성화
        }
    }

    protected virtual void OnBecameInvisible() // 카메라 밖으로 나가면 오브젝트 풀로 반환
    {                               // 다른 함수에서 반환해도 해당 메서드가 실행되기 때문에(비활성화 되면서 카메라에서 사라지는것으로 인식하는듯함)
                                    // Release()가 호출되면 isReleased를 True로 해서 중복실행을 방지함
                                    // 이거 안하니까 같은 오브젝트를 두번씩 반환해서 오브젝트 풀 10칸을 다 쓰고 처음으로 돌아오면
                                    // 같은 오브젝트가 두번씩 호출돼서 한바퀴 돌때마다 두배씩 느려지는 현상이 발생
                                    // 느려지는것뿐만이 아니라 한 오브젝트를 두번 씩 부르니 이것저것 문제가 커서 꼭 신경써야할듯!!
        Release();
    }

    protected void RotateToDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // 이미지가 아래방향이니까 90도 보정

    }
}
