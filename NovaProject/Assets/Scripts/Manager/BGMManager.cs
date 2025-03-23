using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : Singleton<BGMManager>
{
    private AudioSource audioSource;
    public AudioClip BGM1;
    public AudioClip BGM2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM1()
    {
        audioSource.clip = BGM1;
        audioSource.Play();
    }

    public void PlayBGM2()
    {
        audioSource.clip = BGM2;
        audioSource.Play();
    }
}
