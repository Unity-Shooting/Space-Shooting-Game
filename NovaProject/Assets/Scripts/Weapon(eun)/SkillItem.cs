using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public string skillName; // 아이템 이름 (Missile, Laser, Bomb)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 스킬 해금 메서드 호출
            WeaponManager.Instance.UnlockSkill(skillName);

            Debug.Log($"{skillName} 스킬이 해금되었습니다!");

            // 아이템 파괴
            Destroy(gameObject);
        }
    }
}
