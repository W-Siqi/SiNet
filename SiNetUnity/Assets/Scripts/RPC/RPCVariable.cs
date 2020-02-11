using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    public class RPCVariable
    {
        public List<float> floatValues;
        public List<string> stringValues;

        public RPCVariable() {
            floatValues = new List<float>();
            stringValues = new List<string>();
        }
    }
}