using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    private AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip shootSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSound);
    }

    public void ShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
}
