using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class PerspectiveSystem : MonoBehaviour
{
    public enum PerspectiveType {
        patient,nurse,none
    }

    public enum PerspectiveEvent
    {
        openWindow
    }

    [System.Serializable]
    public class ResourceTable{
        public WindowOfWeather windowOfWeather;
    }

    public PerspectiveType selectedPespective = PerspectiveType.none;
    public ResourceTable resourceTable;

    public static PerspectiveSystem _instance = null;

    public static PerspectiveSystem instance {
        get {
            if (_instance)
                return _instance;
            else
                // for the first frame, when Start() not called
                return FindObjectOfType<PerspectiveSystem>();
        }
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else {
            Destroy(this);
            Debug.LogError("multi singleton");
        }
    }

    public void InitPerspective(PerspectiveType type) {
        if (selectedPespective == PerspectiveType.none)
        {
            selectedPespective = type;
        }
        else {
            Debug.LogError("try to change perspective in an unproper case");
        }
    }

    public void ExecutePerspectiveEvent(PerspectiveEvent perspectiveEvent) {
        switch (perspectiveEvent)
        {
            case PerspectiveSystem.PerspectiveEvent.openWindow:
                OnWindowOpened();
                break;
            default:
                Debug.LogError("undeclared event type");
                break;
        }
    }

    private void OnWindowOpened() {
        if (selectedPespective == PerspectiveType.nurse)
        {
            resourceTable.windowOfWeather.OpenToSunny();
        }
        else {
            resourceTable.windowOfWeather.OpenToRain();
        }
    }
}
