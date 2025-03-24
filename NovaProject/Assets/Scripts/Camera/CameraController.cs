using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Cinemachine ī�޶� �迭 (���� ���� ī�޶� ����)
    /// </summary>
    public CinemachineCamera[] ccs;

    /// <summary>
    /// ���� Ȱ��ȭ�� ī�޶� ��ȣ (0���� ����)
    /// </summary>
    public int cameraNum = 0;

    /// <summary>
    /// ������ Ȱ��ȭ�� ī�޶� ��ȣ (������ �����ϱ� ���� ���)
    /// </summary>
    private int pastCameraNum = 0;

    void Update()
    {
        if (pastCameraNum != cameraNum) // ���� ī�޶�� ���� ī�޶� ��ȣ�� �ٸ��� ī�޶� ����
        {
            pastCameraNum = cameraNum;  // ���� ī�޶� ��ȣ�� ���� ��ȣ�� ������Ʈ
            changeCamera(cameraNum);    // ī�޶� ���� �Լ� ȣ��
        }
    }

    /// <summary>
    /// ������ ī�޶� ��ȣ�� ���� ī�޶� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="num">Ȱ��ȭ�� ī�޶� ��ȣ</param>
    void changeCamera(int num)
    {
        for (int i = 0; i < ccs.Length; i++) // ��� ī�޶� ��ȸ�ϸ� Ȱ��ȭ/��Ȱ��ȭ ó��
        {
            if (i == num) ccs[i].Priority = 5; // ���� ī�޶� ��ȣ�� ��ġ�ϸ� �켱������ 5�� ����
            else ccs[i].Priority = 0; // ������ ī�޶�� �켱������ 0���� ����
        }
    }
}
