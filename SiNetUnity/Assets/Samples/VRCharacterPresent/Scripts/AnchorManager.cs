using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    public static AnchorManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public AnchorPoint dummyPatient;
    public AnchorPoint patientPlayerInit;
    public AnchorPoint nursePlayerStart;
}
