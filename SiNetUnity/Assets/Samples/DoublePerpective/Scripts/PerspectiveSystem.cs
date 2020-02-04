using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class PerspectiveSystem : MonoBehaviour
{
    public enum PerspectiveType {
        patient,nurse,none
    }

    public PerspectiveType selectedPespective = PerspectiveType.none;

    public GameObject perspectiveCharacter=null;
    public GameObject patientCharacterPrefab;
    public GameObject nurseCharacterPrefab;

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
        if (perspectiveCharacter) {
            DestroyImmediate(perspectiveCharacter);
        }

        switch (type) {
            case PerspectiveType.nurse:
                perspectiveCharacter = Instantiate(nurseCharacterPrefab);
                break;
            case PerspectiveType.patient:
                perspectiveCharacter = Instantiate(patientCharacterPrefab);
                break;
            default:
                Debug.LogWarning("may has bug! because the type doesn't has prefab");
                break;
        }

        selectedPespective = type;
    }
}
