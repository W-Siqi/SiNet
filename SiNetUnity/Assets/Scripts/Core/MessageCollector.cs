using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class MessageCollector : MonoBehaviour
    {
        public static MessageCollector _instance = null;

        public static MessageCollector instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                else
                    // for the first frame, when Start() not called
                    return FindObjectOfType<MessageCollector>();
            }
        }

        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogError("multi singleton");
            }
        }

        private List<Message> buffer =  new List<Message>();

        public void SendRPCCall(RPCSignaturePlaceHolder signature) {
            var transmitObj = new TransmitableRPCPlaceHolderInfo();
            transmitObj.InitFromOriginalObejct(signature);
            var body = transmitObj.Encode();
            var message = new Message(Message.Type.RPC, body);
            buffer.Add(message);
        }

        public void Collect(Message message) {
            buffer.Add(message);
        }

        public Message[] ReadMessages() {
            var messages = buffer.ToArray();
            buffer.Clear();
            return messages;
        }
    }
}
