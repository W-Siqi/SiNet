using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class RPCReturnHandle {
        public bool isReady;
        public RPCSignature signature;
        public RPCVariable returnValue;

        public RPCReturnHandle(RPCSignature signature) {
            this.signature = signature;
            isReady = false;
            returnValue = null;
        }
    }
}