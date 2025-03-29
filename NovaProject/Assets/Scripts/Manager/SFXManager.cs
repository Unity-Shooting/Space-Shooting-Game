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
        audioMixer.SetFloat("SFX", Mathf.Log10(0.1f) * 20);
        audioSource.clip = damageSound;
        audioSource.Play();
    }

    public void ShootSound()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(0.08f) * 20);
        audioSource.clip = shootSound;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void ClearSound()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(1.8f) * 20);
        audioSource.clip = clearSound;
        audioSource.Play();
    }
}
