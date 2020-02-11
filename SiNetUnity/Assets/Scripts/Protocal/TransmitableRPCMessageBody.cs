using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class TransmitableRPCMessageBody : TransmitableObject
    {
        public bool isCaller;
        public string seriablzedRPCSignature;
        public string serializedRPCVariable;

        public override void InitFromOriginalObejct(object src)
        {
            if (src is RPCMessageBody)
            {
                var messageBody = src as RPCMessageBody;
                isCaller = messageBody.type == RPCMessageBody.Type.call;
                seriablzedRPCSignature = JsonUtility.ToJson(messageBody.rpcSignature);
                serializedRPCVariable = JsonUtility.ToJson(messageBody.rpcVariable);
            }
            else {
                throw new System.Exception("wrong transmitable type");
            }
        }

        public override object ToOriginalObject()
        {
            var signature = JsonUtility.FromJson<RPCSignature>(seriablzedRPCSignature);
            var variable = JsonUtility.FromJson<RPCVariable>(serializedRPCVariable);
            var type = isCaller ? RPCMessageBody.Type.call : RPCMessageBody.Type.receive;
            return new RPCMessageBody(signature, variable, type);
        }
    }
}