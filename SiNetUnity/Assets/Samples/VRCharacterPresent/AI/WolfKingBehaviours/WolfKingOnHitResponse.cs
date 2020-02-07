using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfKingOnHitResponse : MonoBehaviour{
    private const float FIRE_STATE_SUSTAIN_TIME = 4f;

    [SerializeField]    private GameObject bodyFireVFXRoot;
    [SerializeField]
    private AudioSource getFiredSound;
    [SerializeField]
    private AudioSource getHurtSound;

    private float fireStateEndTime = 0f;
    private ParticleSystem[] firedVFXs;

    // Start is called before the first frame update
    void Start()
    {
        firedVFXs = GetComponentsInChildren<ParticleSystem>();
    }

    public void GetFired()
    {
        StartCoroutine(KeepFiring());
        if (getHurtSound)
            getHurtSound.Play();
    }

    IEnumerator KeepFiring()
    {
        fireStateEndTime = Time.time + FIRE_STATE_SUSTAIN_TIME;

        foreach (var vfx in firedVFXs)
            vfx.Play();
        if (getFiredSound)
            getFiredSound.Play();

        while (Time.time < fireStateEndTime)
        {
            yield return null;
        }

        foreach (var vfx in firedVFXs)
            vfx.Stop();
        if (getFiredSound)
            getFiredSound.Stop();
    }
}
