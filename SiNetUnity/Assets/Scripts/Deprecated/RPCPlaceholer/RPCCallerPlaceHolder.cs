using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class RPCCallerPlaceHolder
    {
        private void Start()
        {
            Test();
        }

        public static void CallRPC(RPCSignaturePlaceHolder signature)
        {
            MessageCollector.instance.SendRPCCall(signature);
        }

        private void Test() {
            var sig = new RPCSignaturePlaceHolder("openw");
            CallRPC(sig);
        }
    }
}