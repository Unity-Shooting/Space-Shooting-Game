public interface IDamageable  // �������� �޴� �͵��� ��ӹ��� �������̽�
{

    public void TakeDamage(int damage);
    bool IsDead { get; }

}