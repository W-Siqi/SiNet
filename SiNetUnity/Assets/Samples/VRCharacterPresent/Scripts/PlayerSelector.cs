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
        Instantiate(nursePlayerPrefab);
        UIMenu.SetActive(false);
    }

    public void SelectPatient()
    {
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
