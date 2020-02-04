using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject menuObject;

    public void OnSelectNurse() {
        menuObject.SetActive(false);
        PerspectiveSystem.instance.InitPerspective(PerspectiveSystem.PerspectiveType.nurse);

    }

    public void OnSelectPatient() {
        menuObject.SetActive(false);
        PerspectiveSystem.instance.InitPerspective(PerspectiveSystem.PerspectiveType.patient);
    }
}
