using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // �ִϸ��̼� �� �����ӿ� �̺�Ʈ�� ȣ��
    public void OnEffectEnd()
    {
        PoolManager.instance.Return(gameObject);
    }
}
