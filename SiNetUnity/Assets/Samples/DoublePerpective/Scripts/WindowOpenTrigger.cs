using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class WindowOpenTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            RPCCallerPlaceHolder.CallRPC(new RPCSignaturePlaceHolder("openw"));
        }
    }
}
