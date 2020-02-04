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
    [SerializeField]
    private Animator windowAnimatior;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenToRain() {
        windowAnimatior.SetTrigger("open");
        audioSource.clip = rainSound;
        audioSource.Play();
    }

    public void OpenToSunny(){
        windowAnimatior.SetTrigger("open");
        audioSource.clip = sunnySound;
        audioSource.Play();
    }
}
