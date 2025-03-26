using UnityEngine;

public class MonsterFighter : Monster
{
    [SerializeField] private GameObject Launcher;
    protected override void StartAfterInit()
    {
        InvokeRepeating("Shoot", AttackStart, AttackSpeed);  // 사격 시작
        
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {
        // 플레이어 위치로 MbBullet 발사

        // 몬스터에서 플레이어를 향하는 단위벡터
        Vector2 toPlayer = (PlayerController.Instance.transform.position - this.transform.position).normalized;



        IBulletInit bullet = PoolManager.instance.Get(Bullet).GetComponent<IBulletInit>();
        bullet.Init(Launcher.transform.position, toPlayer, 0);
    }
}
