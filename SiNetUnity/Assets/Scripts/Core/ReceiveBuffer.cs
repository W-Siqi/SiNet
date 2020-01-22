using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    public class ReceiveBuffer
    {
        private List<Message> messages = new List<Message>();

        public Message[] ReadAllMessages() {
            var readResult = messages.ToArray();
            messages.Clear();
            return readResult;
        }

        public void Write(Message message) {
            messages.Add(message);
        }
    }
}