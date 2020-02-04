using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindowOfWeather : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip rainSound;
    [SerializeField]
    private AudioClip sunnySound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenToRain() {
        audioSource.clip = rainSound;
        audioSource.Play();
    }

    public void OpenToSunny(){
        audioSource.clip = sunnySound;
        audioSource.Play();
    }
}
