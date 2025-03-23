using UnityEngine;

public class BackgroundRGBAControll : MonoBehaviour
{
    //스테이지의 인덱스
    public int stageIndex;
    //색상 변화 속도
    public float colorSpeed = 0.5f;
    //배경화면의 마테리얼 객체를 가져올 변수.
    private Material material;
    //alpha의 초기 값. 0.5로 시작해서 처음에는 보인다.
    private float alpha = 0.5f;
    //rgb의 blue 초기 값.
    //r g b
    //1 0 0 일때 빨간색이고
    //1 0 1 일때 보라색으로 표현된다.
    //blue의 값을 0부터 1까지 증감시키면 빨간색과 보라색으로 점등 시킬 수 있다.
    private float blue = 0f;
    // rgba의 밸류의 변화하는 방향
    private float rgbaDirection = -1f;

    //Awake() 메서드는 프리팹이 인스턴스화 된 직후에 가장 먼저 호출된다.

    private void Awake()
    {
        material = GetComponent<Renderer>().material; //material 변수에 해당 객체의 material 컴포넌트를 가져온다.
    }

    private void Update()
    {
        FlashAlpha();
        RedtoPurple();
    }

    //material의 rgba값을 조정하여 별에 시각적인 효과를 구현한 메서드
    private void FlashAlpha()
    {
        //Stage1에서 호출
        if (stageIndex == 1)
        {
            //해당 객체의 마테리얼의 Color 값 불러오기
            Color colorTemp = material.color;
            // 알파 값을 증가 또는 감소
            alpha += Time.deltaTime * rgbaDirection * colorSpeed;

            //Color.a의 값은 float 0부터 1까지이며, 0에서 투명하고 1에서 완전히 보인다.
            // 알파가 0 또는 1에 도달하면 방향 반전
            if (alpha <= 0f)
            {
                alpha = 0f;
                rgbaDirection = 1f;
            }
            else if (alpha >= 0.5f)
            {
                alpha = 0.5f;
                rgbaDirection = -1f;
            }

            // 마테리얼의 Color에 RGBA적용
            colorTemp.a = alpha;
            material.color = colorTemp;
        }
    }
    private void RedtoPurple()
    {
        if (stageIndex == 2)
        {
            //해당 객체의 마테리얼의 Color 값 불러오기
            Color colorTemp = material.color;

            // Adjust the color transition value
            blue += Time.deltaTime * rgbaDirection * colorSpeed;

            // Reverse direction when hitting limits (0 = Red, 1 = Purple)
            if (blue >= 1f)
            {
                blue = 1f;
                rgbaDirection = -1f;
            }
            else if (blue <= 0f)
            {
                blue = 0f;
                rgbaDirection = 1f;
            }

            // Interpolate between Red (1,0,0) and Purple (1,0,1)
            colorTemp.r = 1f;   // Always full red
            colorTemp.g = 0f;   // No green
            colorTemp.b = blue; // Smoothly transition blue (0 → 1 → 0)

            // Apply color
            material.color = colorTemp;

        }
    }
}

