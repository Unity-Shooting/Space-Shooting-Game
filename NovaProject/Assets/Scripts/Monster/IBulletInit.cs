using UnityEngine;

public interface IBulletInit
{
    /// <summary>
    /// type�� �⺻������ �ƹ����� 0 ������ �ְ� �ʿ��� �Ѿ˿����� ���
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// <param name="type"></param>
    public void Init(Vector2 pos, Vector2 dir, int type);
}