using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet{
    // To really use RPC, need to implement the rules of specific RPC function logic
    // This RPC Framework only responsble for sending RPC message and reciving return value.
    // An example is, in "SiNet::NetworkManager", it calls RPC fuction "GetServerTime" to init the local server time info.

    // The default server code support two "built-in" RPC call
    // GetServerTime(), return value is a float(server Time)
    // BoardcastEvent(),  will boardcast the caller with same variable to every client and server.
    public class RPCStub : MonoBehaviour
    {
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

        public RPCReturnHandle Call(string functionName, RPCVariable parameter) {
			var signature = new RPCSignature(functionName);

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

        protected virtual void ProcessRemoteCall(RPCMessageBody message) {
            throw new System.NotImplementedException();
        }

        protected void ProcessReturnValue(RPCMessageBody message) {
            foreach (var handle in registeredReturnHandles) {
                // find matched handle
                if (handle.signature == message.rpcSignature) {
                    handle.isReady = true;
                    handle.returnValue = message.rpcVariable;

                    registeredReturnHandles.Remove(handle);
                    break;
                }
            }
        }
    }
}