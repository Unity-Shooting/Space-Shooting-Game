using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0; // 현재 점수
    public int CheckScore = 0;
    public int TtabongScore = 0;
    public int HeartScore = 0;
    public Image[] digitImages; // ScoreDigit_1 ~ ScoreDigit_6 참조
    public Image[] CheckScoreDigitImages; // ScoreDigit_1 ~ ScoreDigit_2 참조
    public Image[] TtabongScoreDigitImages; // ScoreDigit_1 ~ ScoreDigit_2 참조
    public Image[] HeartScoreDigitImages; // ScoreDigit_1 ~ ScoreDigit_2 참조
    public Sprite[] numberSprites; // 0~9 숫자 이미지 스프라이트 배열

    private void Awake()
    {
        if (instance == null)    //정적으로 자신을 체크 한다.
        {
            instance = this;   //자기 자신을 저장한다.
        }
    }

    void Start()
    {
    }

    void Update()
    {
/*
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddCheckScore(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddTtabongScore(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddHeartScore(1);
        }
*/
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    public void AddCheckScore(int amount)
    {
        Debug.Log("체크 + 1 : ");
        CheckScore += amount;
        HiddenCheckUpdateScore();
    }

    public void AddTtabongScore(int amount)
    {
        Debug.Log("따봉 +1 : ");
        TtabongScore += amount;
        HiddenTtabongUpdateScore();
    }
        public void AddHeartScore(int amount)
    {
        Debug.Log("하트 +1 : ");
        HeartScore += amount;
        HiddenHeartUpdateScore();
    }

    //TestScore()는 말 그대로 테스트용
    /*    public void TestScore()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                score += 100;
                UpdateScore();
            }
        }*/

    void UpdateScore()
    {
        string scoreStr = score.ToString("D6"); // 6자리 숫자로 변환 (000123 등)
        for (int i = 0; i < digitImages.Length; i++)
        {
            int digit = scoreStr[i] - '0'; // 문자 '0'을 빼서 정수 변환
            Convert.ToInt32(scoreStr[i]);
            Debug.Log(Convert.ToInt32(scoreStr[i]) +"="+Convert.ToInt32(scoreStr[i]));
            /*
             * Convert.ToInt32(scoreStr[i])를 쓰게 되면 아스키값으로 나와서 원하는 숫자로 변환이 안됨
             * '1' -> 아스키값 : 49
             * '0' -> 아스키값 : 48
             *  scoreStr[i] - '0';  만약 scoreStr[1] = '1'이면
             *  ->49 - 48
             *  = 1
             */
            digitImages[i].sprite = numberSprites[digit];
        }
    }


    void HiddenCheckUpdateScore()
    {
        string CheckScoreStr = CheckScore.ToString("D2"); // 2자리 숫자로 변환 (01 등)

        for (int i = 0; i < CheckScoreDigitImages.Length; i++)
        {
            if (i >= CheckScoreStr.Length) break; // 배열 길이를 초과하지 않도록 방지

            int digit = CheckScoreStr[i] - '0'; // 문자 '0'을 빼서 정수 변환
            CheckScoreDigitImages[i].sprite = numberSprites[digit];

            Debug.Log($"digitImages[{i}] = {digit}");
        }
    }
    void HiddenTtabongUpdateScore()
    {
        string TtabongScoreStr = TtabongScore.ToString("D2"); // 2자리 숫자로 변환 (01 등)

        for (int i = 0; i < TtabongScoreDigitImages.Length; i++)
        {
            if (i >= TtabongScoreStr.Length) break; // 배열 길이를 초과하지 않도록 방지

            int digit = TtabongScoreStr[i] - '0'; // 문자 '0'을 빼서 정수 변환
            TtabongScoreDigitImages[i].sprite = numberSprites[digit];

            Debug.Log($"digitImages[{i}] = {digit}");
        }
    }
    void HiddenHeartUpdateScore()
    {
        string HeartScoreStr = HeartScore.ToString("D2"); // 2자리 숫자로 변환 (01 등)

        for (int i = 0; i < HeartScoreDigitImages.Length; i++)
        {
            if (i >= HeartScoreStr.Length) break; // 배열 길이를 초과하지 않도록 방지

            int digit = HeartScoreStr[i] - '0'; // 문자 '0'을 빼서 정수 변환
            HeartScoreDigitImages[i].sprite = numberSprites[digit];

            Debug.Log($"digitImages[{i}] = {digit}");
        }
    }

}