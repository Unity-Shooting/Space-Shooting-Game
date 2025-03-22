using System.Collections;
using UnityEngine;

/// <summary>
/// �÷��̾��� ü���� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    private const string TAG = "Health";

    /// <summary>
    /// �÷��̾��� ü���� �����ϴ� �����Դϴ�.
    /// </summary>
    public int hp;

    public float recoveryTime = 1f;

    private bool canTakeDamage = true;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �ʱ�ȭ�� �� ȣ��˴ϴ�.
    /// <para>Awake()���� GameManager�� �α� ���¸� üũ�ϰ� �α׸� ����� �� �ֽ��ϴ�.</para>
    /// </summary>
    protected override void Awake()
    {
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake before base.Awake");
        base.Awake();
        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] Awake after base.Awake");
    }

    /// <summary>
    /// �÷��̾ �������� ���� �� ȣ��Ǵ� �޼����Դϴ�.
    /// <para>ü���� �����ϰ�, �α׷� ü�� ���¸� ����մϴ�.</para>
    /// </summary>
    /// <param name="num">�Էµ� ������ ��ġ</param>
    public void TakeDamage(int num)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        if (--hp < 1) hp = 0; // ü�� ���� �� �ּҰ� 0���� ����
        //if (GameManager.Instance.logOn) Debug.Log($"[{TAG}] hp : {hp}"); // ü�� ���� �� �α� ���
        Flash flash = PlayerController.Instance.Base.gameObject.GetComponent<Flash>();
        flash.Run();
        StartCoroutine(RecoveryRoutine());
        //PlayerController.Instance.FrontSideShield.gameObject.SetActive(true);
    }

    IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(recoveryTime);
        canTakeDamage = true;
        //PlayerController.Instance.FrontSideShield.gameObject.SetActive(false);
    }

    /// <summary>
    /// �浹 ���� �� ����Ǵ� �Լ�.
    /// </summary>
    /// <param name="collision">�浹�� ��ü�� Collider2D</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: ȭ�� ��鸲 ȿ�� ����

        //if(GameManager.Instance.logOn) Debug.Log($"[{TAG}] OnTriggerEnter2D. tag : {collision.gameObject.tag}");

        if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(collision.gameObject);
            TakeDamage(1); // ü�� ����
        }
    }
}
