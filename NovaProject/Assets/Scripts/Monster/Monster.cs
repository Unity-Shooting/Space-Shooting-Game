using UnityEngine;

public abstract class Monster : MonoBehaviour
{

    private int HP;  // 최대 체력이랑 구분해둔 이유 : Init()로 초기화 하기 위해

    [SerializeField]  // 인스펙터에서 조절할 수치들은 스크립트에서 초기값 안넣기(헷갈림)
    protected int MaxHP;
    [SerializeField]
    protected int Attack;
    [SerializeField]
    protected float MoveSpeed;

    public abstract void Shoot();
    public abstract void Move(Vector2 dir);

    public virtual void Damaged(int damage)
    {
        HP -= damage;
        Debug.Log($"{this.name} damaged : {damage} HP : {HP}");
        if (HP < 0)
        {
            Debug.Log($"{this.name} destroyed");
            Die();
        }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
        //오브젝트풀로 바꿔보기
    }

}
