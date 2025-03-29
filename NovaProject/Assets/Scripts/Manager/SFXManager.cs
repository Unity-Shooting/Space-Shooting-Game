using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : Singleton<SFXManager>
{
    private AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip shootSound;
    public AudioClip clearSound;
    public AudioMixer audioMixer;


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

    public void ClearSound()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(1f) * 20);
        audioSource.clip = clearSound;
        audioSource.Play();
    }
}
