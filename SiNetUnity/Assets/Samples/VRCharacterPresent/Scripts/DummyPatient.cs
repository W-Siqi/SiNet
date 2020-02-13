using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPatient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var initPos = AnchorManager.instance.dummyPatient.transform.position;
        transform.position = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
