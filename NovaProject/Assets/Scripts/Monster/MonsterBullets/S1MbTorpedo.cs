using UnityEngine;
using UnityEngine.WSA;

public class S1MbTorpedo : MbBase, IDamageable
{
    [SerializeField] private float hoamingPower;
    [SerializeField] private float maxHoamingAngle;
    [SerializeField] private float maxHP;
    private float HP;
    private bool isDead = false;
    public bool IsDead => isDead;

    void Update()
    {
        Hoaming();
        Move();
    }
    /// <summary>
    /// 플레이어 방향으로 유도하는 함수
    /// 총알이 가진 vector2 direction을 수정해서 진행 방향을 바꾸고
    /// 이미지도 해당 각도로 회전시켜서 자연스럽게 유도할수있도록 시도해봄
    /// 유도인 대신 플레이어 총알로 격추할 수 있도록 함
    /// 플레이어를 지나친 미사일이 밑에서 올라오지 않도록 direction의 각도를 y축기준 좌우 maxDegree도로 제한
    /// </summary>
    private void Hoaming()
    {
        // 로켓에서 플레이어를 향하는 단위벡터
        Vector2 toPlayer = (PlayerController.Instance.transform.position - this.transform.position).normalized;
        // 아래방향 단위벡터와의 각도가 maxDegree 이하인 경우 유도
        float angleToDown = Vector2.SignedAngle(Vector2.down, toPlayer);
        if(Mathf.Abs(angleToDown) < maxHoamingAngle)
        {
            direction = Vector2.Lerp(direction, toPlayer, hoamingPower * Time.deltaTime);
        }
        // 미사일 스프라이트를 회전시켜주기
        RotateToDirection();

    }

    public void TakeDamage(int  damage)
    {
        

        HP -= damage;
        if (HP <= 0)
        {
            if (isDead) return;
            isDead = true;
            Release();
        }
    }
    protected override void OnEnable()
    {
        // 다른 총알들은 가지지 않는 체력을 가지기 때문에 따로 초기화
        base.OnEnable();
        HP = maxHP;
        isDead = false;
    }

}
