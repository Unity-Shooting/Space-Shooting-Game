public interface IDamageable  // 데미지를 받는 것들이 상속받을 인터페이스
{

    public void TakeDamage(int damage);
    bool IsDead { get; }

}