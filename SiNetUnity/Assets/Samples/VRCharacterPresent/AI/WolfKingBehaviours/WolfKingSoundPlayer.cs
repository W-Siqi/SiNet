using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WolfKingSoundPlayer : MonoBehaviour
{
    public AudioClip growl;
    public AudioClip howling;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip) {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
