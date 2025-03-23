using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// �÷��̾��� ü���� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    private const string TAG = "Health"; // �α� ��� �� ���� �±�

    /// <summary>
    /// �÷��̾��� ��������Ʈ�� �����ϴ� �����Դϴ�.
    /// </summary>
    public Sprite[] baseSprites;

    /// <summary>
    /// �÷��̾��� ���� ü���� �����ϴ� �����Դϴ�.
    /// </summary>
    public int hp;

    /// <summary>
    /// ������� ���� �� �ٽ� ������� ���� �� �ֵ��� ȸ���Ǵ� �ð�(�� ����)
    /// </summary>
    public float invincibleTime = 1f;

    /// <summary>
    /// ���� ������� ���� �� �ִ��� ���θ� ��Ÿ���� �÷���
    /// </summary>
    private bool canTakeDamage = true;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �ʱ�ȭ�� �� ȣ��˴ϴ�.
    /// <para>Awake()���� GameManager�� �α� ���¸� üũ�ϰ� �α׸� ����� �� �ֽ��ϴ�.</para>
    /// </summary>
    protected override void Awake()
    {
        // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake before base.Awake");
        base.Awake(); // �̱��� ���Ͽ��� �⺻ Awake ����
        // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake after base.Awake");
    }

    /// <summary>
    /// �÷��̾ ������� ���� �� ȣ��Ǵ� �޼����Դϴ�.
    /// <para>ü���� �����ϰ�, ȭ�� ȿ�� �� ���� �ð� ó���� �մϴ�.</para>
    /// </summary>
    /// <param name="num">�Էµ� ����� ��</param>
    public void TakeDamage(int num)
    {
        if (!canTakeDamage) return; // ���� ���¶�� ������� ���� ����
        canTakeDamage = false; // ������� �޾����Ƿ� ���� ���·� ����

        // hp�� ���� sprite ����
        if (--hp < 1) hp = 0; // ü�� ���� �� 0 ���Ϸ� �������� �ʵ��� ����
        SpriteRenderer pSR = PlayerController.Instance.Base.GetComponent<SpriteRenderer>();
        if (hp == 0)
        {
            pSR.color = Color.red;
            GameManager.Instance.GameOver();
        }
        else if (hp < 3) pSR.sprite = baseSprites[3];
        else if (hp < 6) pSR.sprite = baseSprites[2];
        else if (hp < 9) pSR.sprite = baseSprites[1];
        else pSR.sprite = baseSprites[0];

        // if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] hp : {hp}"); // ü�� ���� �� �α� ���
        PlayerController.Instance.Base.gameObject.GetComponent<Flash>().Run(); // �÷��̾ �ǰ� �� �����̴� ȿ�� ����
        StartCoroutine(RecoveryRoutine()); // ���� �ð� �� �ٽ� ������� ���� �� �ֵ��� ���� ����
        ScreenShakeManager.Instance.ShakeScreen(); // ȭ�� ��鸲 ȿ�� ����
    }

    /// <summary>
    /// ���� �ð��� ������ �ٽ� ������� ���� �� �ֵ��� �����ϴ� �ڷ�ƾ
    /// </summary>
    IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(invincibleTime); // ȸ�� �ð���ŭ ���
        canTakeDamage = true; // �ٽ� ������� ���� �� �ֵ��� ����
    }

    /// <summary>
    /// �浹 ���� �� ����Ǵ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="collision">�浹�� ��ü�� Collider2D ����</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] OnTriggerEnter2D. tag : {collision.gameObject.tag}");
        if (collision.gameObject.tag == "Bullet") // �浹�� ��ü�� �Ѿ�(Bullet)�� ��� ������� ����
        {
            // Destroy(collision.gameObject); // �Ѿ� ��ü ���� (�ּ� ó����)
            TakeDamage(1); // ü�� 1 ����
        }
    }
}
