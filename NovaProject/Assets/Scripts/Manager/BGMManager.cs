using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : Singleton<BGMManager>
{
    private AudioSource audioSource;
    public AudioClip BGM1;
    public AudioClip BGM2;
    public AudioClip BGMStage2;
    public AudioClip BGMHidden;
    public AudioMixer audioMixer;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
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

    public void PlayBGMStage2()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(0.5f) * 20);
        audioSource.clip = BGMStage2;
        audioSource.Play();
    }
    public void PlayBGMHidden()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(0.5f) * 20);
        audioSource.clip = BGMHidden;
        audioSource.Play();
    }
}
