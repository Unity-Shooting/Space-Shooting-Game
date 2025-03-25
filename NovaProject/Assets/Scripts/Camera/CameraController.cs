using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Cinemachine 카메라 배열 (여러 개의 카메라 설정) 
    /// </summary>
    public CinemachineCamera[] ccs;

    /// <summary>
    /// 현재 활성화된 카메라 번호 (0부터 시작)
    /// </summary>
    public int cameraNum = 0;

    /// <summary>
    /// 이전에 활성화된 카메라 번호 (변경을 추적하기 위해 사용)
    /// </summary>
    private int pastCameraNum = 0;

    void Update()
    {
        if (pastCameraNum != cameraNum) // 이전 카메라와 현재 카메라 번호가 다르면 카메라를 변경
        {
            pastCameraNum = cameraNum;  // 이전 카메라 번호를 현재 번호로 업데이트
            changeCamera(cameraNum);    // 카메라 변경 함수 호출
        }
    }

    /// <summary>
    /// 지정된 카메라 번호에 맞춰 카메라를 전환하는 함수
    /// </summary>
    /// <param name="num">활성화할 카메라 번호</param>
    void changeCamera(int num)
    {
        for (int i = 0; i < ccs.Length; i++) // 모든 카메라를 순회하며 활성화/비활성화 처리
        {
            if (i == num) ccs[i].Priority = 5; // 현재 카메라 번호와 일치하면 우선순위를 5로 설정
            else ccs[i].Priority = 0; // 나머지 카메라는 우선순위를 0으로 설정
        }
    }
}
