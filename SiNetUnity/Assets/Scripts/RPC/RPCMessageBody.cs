using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class RPCMessageBody
    {
        public enum Type {
            call, receive
        }

        public Type type;
        public RPCSignature rpcSignature;
        public RPCVariable rpcVariable;

        public RPCMessageBody(RPCSignature signature, RPCVariable variable, Type type) {
            this.type = type;
            this.rpcSignature = signature;
            this.rpcVariable = variable;
        }
    }
}