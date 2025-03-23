using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : Singleton<BGMManager>
{
    private AudioSource audioSource;
    public AudioClip BGM1;
    public AudioClip BGM2;
    public AudioMixer audioMixer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM1()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(1f) * 20);
        audioSource.clip = BGM1;
        audioSource.Play();
    }

    public void PlayBGM2()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(5f) * 20);
        audioSource.clip = BGM2;
        audioSource.Play();
    }
}
