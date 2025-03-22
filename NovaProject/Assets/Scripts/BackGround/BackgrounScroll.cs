using System.Collections;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    //배경화면의 마테리얼 객체를 가져올 변수.
    private Material material;
    //배경화면의 이동속도.
    public float scrollSpeed = 1f;

    //Awake() 메서드는 프리팹이 인스턴스화 된 직후에 가장 먼저 호출된다.
    //material 변수에 해당 객체의 material 컴포넌트를 가져온다.
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    
    private void Update()
    {
        MapScroll();
    }

    //material 객체의 offset을 증가시켜 맵을 Y축의 +방향으로 이동시킨다.
    private void MapScroll()
    {
        Vector2 offset = material.mainTextureOffset;
        offset.y += scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}
