using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera[] ccs;

    public int cameraNum = 0;
    private int pastCameraNum = 0;

    void Update()
    {
        if (pastCameraNum != cameraNum)
        {
            pastCameraNum = cameraNum;
            changeCamera(cameraNum);
        }

    }

    void changeCamera(int num)
    {
        for(int i=0; i<ccs.Length; i++)
        {
            if (i == num) ccs[i].Priority = 5;
            else ccs[i].Priority = 0;
        }
    }
}
