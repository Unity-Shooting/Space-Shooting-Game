using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PWRManager : MonoBehaviour
{

    public Image Pwr;
    public Image Hp;
    

    void Start()
    {

       // ScoreManager.instance.AddScore(100); ScoreManager �ν��Ͻ� �׽�Ʈ
    }


    private void Update()
    {
        UpdatePwrBar();  //�׽�Ʈ
        UpdateHpHeart();
    }

  public void UpdatePwrBar()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Pwr.fillAmount > 0.845f)
        {
            Pwr.fillAmount -= this.Pwr.fillAmount;
        }
        if(Pwr.fillAmount != 1f)
        {
            Pwr.fillAmount  += 0.001f;
        }
    }

    public void UpdateHpHeart()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Hp.fillAmount -= 0.25f;
        }
    }

}
