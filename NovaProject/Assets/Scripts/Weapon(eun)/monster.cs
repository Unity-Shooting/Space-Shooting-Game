using UnityEngine;

public class monster : MonoBehaviour
{
    public int health = 100; // 몬스터 체력

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"몬스터가 {damage}의 피해를 입었습니다. 남은 체력: {health}");
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
        float tickDamage = 5; // 초당 피해량
        float interval = 1.0f; // 초당 간격

        while (duration > 0)
        {
            TakeDamage((int)tickDamage);
            duration -= interval;
            yield return new WaitForSeconds(interval);
        }
    }

    private void Die()
    {
        Debug.Log("몬스터가 죽었습니다!");
        Destroy(gameObject);
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
