using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundEffectSourceAmbient;

    public AudioSource soundEffectSource;

    public AudioClip dead;
    public AudioClip jump;
    public AudioClip ambient;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // this works to still have the sound active between scenes
    }

    public void PlayDeadSound()
    {
        soundEffectSource.PlayOneShot(dead);
    }

    public void PlayJumpSound()
    {
        soundEffectSource.PlayOneShot(jump);
    }

    public void PlayAmbientSoundLoop()
    {
        soundEffectSourceAmbient.clip = ambient;
        soundEffectSourceAmbient.loop = true;
        soundEffectSourceAmbient.volume = 0.2f;
        soundEffectSourceAmbient.Play();
    }

    public void PlayGameOverSound()
    {
        //soundEffectSource.PlayOneShot(gameOverSound);
    }
}