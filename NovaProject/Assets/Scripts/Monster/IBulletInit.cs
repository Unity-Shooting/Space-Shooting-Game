using UnityEngine;

public interface IBulletInit
{
    /// <summary>
    /// type는 기본적으로 아무숫자 0 같은거 넣고 필요한 총알에서만 사용
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// <param name="type"></param>
    public void Init(Vector2 pos, Vector2 dir, int type);
}