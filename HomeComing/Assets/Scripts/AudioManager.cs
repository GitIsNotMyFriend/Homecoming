using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] clips;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    void Start()
    {
        foreach (AudioClip audio in clips)
            audioClips.Add(audio.name, audio);

        audioSource = this.GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Play(string clipName)
    {
        audioSource.clip = audioClips[clipName];
        audioSource.Play();
    }
}
