using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class RPCReceiverPlaceHolder
    {
        public static void ReceiveRPC(RPCSignaturePlaceHolder signature)
        {
            Debug.Log("RPC receive: " + signature.name);
            if(signature.name == "openw")
            {
                PerspectiveSystem.instance.TriggerPespectiveEvent(PerspectiveSystem.Event.openWindow);
            }
        }
    }
}