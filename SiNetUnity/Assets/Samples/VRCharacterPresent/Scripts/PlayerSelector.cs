using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject UIMenu;
    public GameObject nursePlayerPrefab;
    public GameObject patientPlayerPrefab;
    public GameObject observerPrefab;

    public void SelectNurse() {
        PerspectiveSystem.instance.InitPerspective(PerspectiveSystem.PerspectiveType.nurse);

        var nurseGO = Instantiate(nursePlayerPrefab);
        UIMenu.SetActive(false);
        var initPos = AnchorManager.instance.nursePlayerStart.transform.position;
        nurseGO.transform.position = initPos;
    }

    public void SelectPatient()
    {
        PerspectiveSystem.instance.InitPerspective(PerspectiveSystem.PerspectiveType.patient);

        var patientGO = Instantiate(patientPlayerPrefab);
        UIMenu.SetActive(false);
        var initPos = AnchorManager.instance.patientPlayerInit.transform.position;
        patientGO.transform.position = initPos;
    }

    public void SelectObserver()
    {
        Instantiate(observerPrefab);
        UIMenu.SetActive(false);
    }
}
