using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float ScrollSpeed = 1f;
    private Material thisMaterial;

    void Awake()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector2 newoffset = thisMaterial.mainTextureOffset;
        newoffset.Set(0,newoffset.y + (ScrollSpeed * Time.deltaTime));
        thisMaterial.mainTextureOffset = newoffset;
    }
}
