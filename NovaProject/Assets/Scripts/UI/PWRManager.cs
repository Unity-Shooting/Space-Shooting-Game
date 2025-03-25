using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PWRManager : MonoBehaviour
{

    public Image Pwr;
    public Image Ammo;
    public Image[] images;

    public float duration = 10f; // 10초 동안 fillAmount 증가



    void Start()
    {
        HideAllImages();
        ShowImage(1);
        // ScoreManager.instance.AddScore(100); ScoreManager 인스턴스 테스트
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(FillOverTime(duration));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(FillOverTimeAmmo(duration));
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) ShowImage(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ShowImage(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ShowImage(3);

    }

    public void ShowImage(int index)
    {
        HideAllImages(); // 모든 이미지를 비활성화

        switch (index)
        {
            case 1:
                images[0].gameObject.SetActive(true);
                break;
            case 2:
                images[1].gameObject.SetActive(true);
                break;
            case 3:
                images[2].gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("잘못된 번호입니다.");
                break;
        }
    }

    void HideAllImages()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
    }

    IEnumerator FillOverTime(float time)
    {
        Pwr.fillAmount = 0f;
        float elapsedTime = 0f; // 경과 시간 초기화
        while (elapsedTime < time)
        {
            Pwr.fillAmount = elapsedTime / time; // FillAmount 비율 계산
            elapsedTime += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
        Pwr.fillAmount = 1f; // 10초가 되면 fillAmount를 정확히 1로 설정
    }

    IEnumerator FillOverTimeAmmo(float time)
    {
        Ammo.fillAmount = 0f;
        float elapsedTime = 0f; // 경과 시간 초기화
        while (elapsedTime < time)
        {
            Ammo.fillAmount = elapsedTime / time; // FillAmount 비율 계산
            elapsedTime += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
        Ammo.fillAmount = 1f; // 10초가 되면 fillAmount를 정확히 1로 설정
    }

}
