using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{

    public AudioClip[] audioClip = new AudioClip[0];
    private AudioSource audioSource;

    public void soundshot(int number)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip[number];
        audioSource.PlayOneShot(audioSource.clip);
    }
   
}
