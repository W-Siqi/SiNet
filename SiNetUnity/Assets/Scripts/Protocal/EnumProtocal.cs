using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class EnumProtocal
    {
        const string MESSAGE_TYPE_SYNC = "sync";
        const string MESSAGE_TYPE_RPC = "RPC";
        const string MESSAGE_TYPE_NONE = "none";

        public static string Encode(Message.Type type) {
            var str = "";
            switch (type) {
                case Message.Type.syncMessage:
                    str = MESSAGE_TYPE_SYNC;
                    break;
                case Message.Type.none:
                    str = MESSAGE_TYPE_NONE;
                    break;
                case Message.Type.RPC:
                    str = MESSAGE_TYPE_RPC;
                    break;
                default:
                    Debug.LogError("UNDEFINED TYPE");
                    break;
            }
            return str;
        }

        public static Message.Type Decode(string src) {
            var type = Message.Type.none;
            switch (src) {
                case MESSAGE_TYPE_SYNC:
                    type = Message.Type.syncMessage;
                    break;
                case MESSAGE_TYPE_NONE:
                    type = Message.Type.none;
                    break;
                case MESSAGE_TYPE_RPC:
                    type = Message.Type.RPC;
                    break;
                default:
                    Debug.LogError("UNDEFINED TYPE");
                    break;
            }
            return type;
        }
    }
}
