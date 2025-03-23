using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // 애니메이션 끝 프레임에 이벤트로 호출
    public void OnEffectEnd()
    {
        PoolManager.instance.Return(gameObject);
    }
}
