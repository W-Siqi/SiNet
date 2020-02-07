using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPVisualIndicator : MonoBehaviour
{
    [SerializeField]
    private RuntimeMaterial attachedMat;
    [SerializeField]
    [Range(0,1)]
    private float curHPRate = 1f;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color dieColor;
    [SerializeField]
    private float offsetBias = 0.1f;
    [SerializeField]
    private float maxSpeedFactor = 7f;
    [SerializeField]
    private float minSpeedFactor = 2.5f;

    private float accumulativePhrase = 0;
    public void SyncHPRate(float hpRate) {
        curHPRate = Mathf.Clamp(hpRate, 0, 1);
    }
    
    void Update()
    {
        HPVisualUpdate();
    }

    private void HPVisualUpdate() {
        if (!attachedMat.RTMat)
            return;

        if (curHPRate == 0) {
            attachedMat.RTMat.SetColor("_EmissionColor", Color.black);
        }

        var speed = Mathf.Lerp(maxSpeedFactor, minSpeedFactor, curHPRate);
        accumulativePhrase += speed * Time.deltaTime;

        var curLightVal = curHPRate + offsetBias * Mathf.Sin(accumulativePhrase);
        curLightVal = Mathf.Clamp(curLightVal, 0f, 1f);

        var curColor = Color.Lerp(dieColor, fullColor, curLightVal);
        attachedMat.RTMat.SetColor("_EmissionColor",1.5f * curColor);
    }
}
