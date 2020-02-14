using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindowOfWeather : MonoBehaviour
{
    public bool isOpen = false;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip rainSound;
    [SerializeField]
    private AudioClip sunnySound;
    [SerializeField]
    private Animator windowAnimatior;

    [SerializeField]
    private MeshRenderer skyboxRenderer;
    [SerializeField]
    private Material rainSkyboxMat;
    [SerializeField]
    private Material sunnySkyboxMat;
    [SerializeField]
    private GameObject rainEffect;

    [SerializeField]
    private Light lightSource;
    [SerializeField]
    private Color sunlightColor;
    [SerializeField]
    private float sunlightMaxIntensity = 1.8f;
    [SerializeField]
    private Color rainlightColor;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenToRain() {
        isOpen = true;

        windowAnimatior.SetTrigger("open");

        // set bgm
        audioSource.clip = rainSound;
        audioSource.Play();

        // set sky box
        skyboxRenderer.sharedMaterial = rainSkyboxMat;

        // set rain
        rainEffect.transform.localPosition = Vector3.zero;

        // set lighting
        lightSource.color = rainlightColor;
    }

    public void OpenToSunny(){
        isOpen = true;

        windowAnimatior.SetTrigger("open");

        audioSource.clip = sunnySound;
        audioSource.Play();

        // set sky box
        skyboxRenderer.sharedMaterial = sunnySkyboxMat;

        // set sun light
        lightSource.color = sunlightColor;
        StartCoroutine(BecomeBright());
    }

    IEnumerator BecomeBright() {
        float sustainTime = 4f;
        float startTime = Time.time;
        float startIntensity = lightSource.intensity;
        while (Time.time < startTime + sustainTime) {
            var t = (Time.time - startTime) / sustainTime;
            lightSource.intensity = Mathf.Lerp(startIntensity, sunlightMaxIntensity, t);
            yield return null;
        }
    }
}
