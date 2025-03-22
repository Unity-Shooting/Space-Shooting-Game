using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0; // ���� ����
    public Image[] digitImages; // ScoreDigit_1 ~ ScoreDigit_6 ����
    public Sprite[] numberSprites; // 0~9 ���� �̹��� ��������Ʈ �迭

    private void Awake()
    {
        if (instance == null)    //�������� �ڽ��� üũ �Ѵ�.
        {
            instance = this;   //�ڱ� �ڽ��� �����Ѵ�.
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        TestScore();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }
    
    //TestScore()�� �� �״�� �׽�Ʈ��
    public void TestScore()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 100;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        string scoreStr = score.ToString("D6"); // 6�ڸ� ���ڷ� ��ȯ (000123 ��)
        for (int i = 0; i < digitImages.Length; i++)
        {
            int digit = scoreStr[i] - '0'; // ���� '0'�� ���� ���� ��ȯ
            Convert.ToInt32(scoreStr[i]);
            Debug.Log(Convert.ToInt32(scoreStr[i]) +"="+Convert.ToInt32(scoreStr[i]));
            /*
             * Convert.ToInt32(scoreStr[i])�� ���� �Ǹ� �ƽ�Ű������ ���ͼ� ���ϴ� ���ڷ� ��ȯ�� �ȵ�
             * '1' -> �ƽ�Ű�� : 49
             * '0' -> �ƽ�Ű�� : 48
             *  scoreStr[i] - '0';  ���� scoreStr[1] = '1'�̸�
             *  ->49 - 48
             *  = 1
             */
            digitImages[i].sprite = numberSprites[digit];
        }
    }
}