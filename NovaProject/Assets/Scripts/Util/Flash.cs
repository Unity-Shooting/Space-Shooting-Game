using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public float flashDuration = .1f;
    public int refeatCnt = 3;
    public Material flashMat;

    private SpriteRenderer sr;
    private Material defaultMat;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
    }


    public void Run()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for(int i=0; i<refeatCnt; i++)
        {
            sr.material = flashMat;
            yield return new WaitForSeconds(flashDuration);
            sr.material = defaultMat;
            yield return new WaitForSeconds(flashDuration);
        }
    }

}
