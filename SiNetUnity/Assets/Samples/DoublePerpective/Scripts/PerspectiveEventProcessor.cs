using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerspectiveEventProcessor
{
    public WindowOfWeather weatherWindow;

    public void ProcessEvent(PerspectiveSystem.Event perspectiveEvent) {
        switch (perspectiveEvent) {
            case PerspectiveSystem.Event.openWindow:
                OnWindowOpened();
                break;
            default:
                Debug.LogError("un declared event type");
                break;
        }
    }

    private void OnWindowOpened() {
        var perspective = PerspectiveSystem.instance.selectedPespective;
        if (perspective == PerspectiveSystem.PerspectiveType.patient)
        {
            weatherWindow.OpenToRain();
            SubtitlePlayer.instance.PlaySubtitle(1, 4f);
        }
        else {
            weatherWindow.OpenToSunny();
            SubtitlePlayer.instance.PlaySubtitle(2, 4f);
        }
    }
}
