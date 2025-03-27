using UnityEngine;

public class BossWeaponController : MonoBehaviour
{
    private string TAG = "BossWeaponController";
    public GameObject text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Shoot", 3.0f, 3.0f);
    }

    void Shoot()
    {
        Instantiate(text, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
