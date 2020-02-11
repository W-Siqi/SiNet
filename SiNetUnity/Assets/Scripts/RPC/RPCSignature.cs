using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    public class RPCSignature
    {
        public string name;
        public string callerIP;
        // the calledTime can only ensure the sequence: the latter one is not small than the former
        public float calledTime;

        public RPCSignature(string name) {
            this.name = name;
            this.callerIP = GetLocalIP();
            this.calledTime = ServerTime.current;
        }

        private RPCSignature() {

        }

        private static string GetLocalIP() {
            var hostname = Dns.GetHostName();
            var ipEntry = System.Net.Dns.GetHostEntry(hostname);
            var addressList = ipEntry.AddressList;
            if (addressList.Length > 0)
            {
                return addressList[0].ToString();
            }
            else {
                return "";
            }
        }

        public static bool operator ==(RPCSignature s1, RPCSignature s2) {
            return s1.name == s2.name && s1.callerIP == s2.callerIP && s1.calledTime == s2.calledTime;
        }

        public static bool operator !=(RPCSignature s1, RPCSignature s2)
        {
            return !(s1 == s2);
        }
    }
}