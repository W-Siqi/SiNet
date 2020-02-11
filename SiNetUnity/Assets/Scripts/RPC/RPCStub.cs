using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet{
    public class RPCStub : MonoBehaviour
    {
        public enum CallableFunction {
            getServerTime
        }

        public enum RecvableFunction
        {
            getServerTime,
            callEvent
        }

        public static RPCStub instance {
            get {
                if (_instance)
                    return _instance;
                else
                    return FindObjectOfType<RPCStub>();
            }
        }

        private static RPCStub _instance;

        private List<RPCReturnHandle> registeredReturnHandles;

        private void Start()
        {
            if (!_instance)
            {
                _instance = this;
            }
            else {
                Debug.LogError("multi - instance");
                Destroy(this);
            }

            registeredReturnHandles = new List<RPCReturnHandle>();
        }

        public RPCReturnHandle Call(CallableFunction function, RPCVariable parameter) {
            var signature = CreateSignaure(function);
            var messageBody = new RPCMessageBody(signature,parameter, RPCMessageBody.Type.call);

            var transmitableBody = new TransmitableRPCMessageBody();
            transmitableBody.InitFromOriginalObejct(messageBody);
            var message = new Message(Message.Type.RPC, transmitableBody.Encode());

            MessageCollector.instance.Collect(message);

            var returnHanlde = new RPCReturnHandle(signature);
            registeredReturnHandles.Add(returnHanlde);
            return returnHanlde;
        }

        public void ProcessRPCMessage(RPCMessageBody message) {
            if (message.type == RPCMessageBody.Type.call){
                ProcessRemoteCall(message);
            }
            else {
                ProcessReturnValue(message);
            }
        }

        private void ProcessRemoteCall(RPCMessageBody message) {
            throw new System.NotImplementedException();
        }

        private void ProcessReturnValue(RPCMessageBody message) {
            foreach (var handle in registeredReturnHandles) {
                if (handle.signature == message.rpcSignature) {
                    handle.isReady = true;
                    handle.returnValue = message.rpcVariable;

                    registeredReturnHandles.Remove(handle);
                    break;
                }
            }
        }

        private RPCSignature CreateSignaure(CallableFunction function) {
            var name = "";
            switch (function) {
                case CallableFunction.getServerTime:
                    name = "GetServerTime";
                    break;
                default:
                    throw new System.Exception("undefined RPC signature");
                    break;
            }

            return new RPCSignature(name);
        }
    }
}