﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SiNet {
	// the common, shared data struct between client and server
	// the encode/decode method (in MessageBodyProtocal) depends on the type of the message 
	public class Message
    {
        public enum Type { none,syncMessage,RPC};

        public float time;

        public string type = "";

        public string body;

        public Message(Type type, string body) {
            this.type = EnumProtocal.Encode(type);
            this.time = Time.time;
            this.body = body;
        }

        private Message() { }

		public byte[] ToBytes() {
            var contentStr = JsonUtility.ToJson(this);
            var dataLen = contentStr.Length; 
            var packageStr = string.Format("%{0}{1}%",dataLen.ToString(),contentStr);
            return  Encoding.ASCII.GetBytes(packageStr);
        }
    }
}