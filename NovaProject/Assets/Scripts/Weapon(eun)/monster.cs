using UnityEngine;

public class monster : MonoBehaviour
{
    public int health = 100; // ���� ü��

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"���Ͱ� {damage}�� ���ظ� �Ծ����ϴ�. ���� ü��: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyStatusEffect(string effectType, float duration)
    {
        if (effectType == "Burn")
        {
            StartCoroutine(BurnEffect(duration));
        }
    }

    private System.Collections.IEnumerator BurnEffect(float duration)
    {
        float tickDamage = 5; // �ʴ� ���ط�
        float interval = 1.0f; // �ʴ� ����

        while (duration > 0)
        {
            TakeDamage((int)tickDamage);
            duration -= interval;
            yield return new WaitForSeconds(interval);
        }
    }

    private void Die()
    {
        Debug.Log("���Ͱ� �׾����ϴ�!");
        Destroy(gameObject);
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
