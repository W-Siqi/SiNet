using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class HotKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            RPCCallerPlaceHolder.CallRPC(new RPCSignaturePlaceHolder("openw"));
        }
    }
}
