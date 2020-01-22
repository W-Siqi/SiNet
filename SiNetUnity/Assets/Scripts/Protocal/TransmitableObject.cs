using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    // this object can be serilized (by json) easily because it only contains strings
    // if we want to use 'class' as the message's body, we firstly need convert to this class
    public abstract class TransmitableObject
    {
        [System.Serializable]
        protected class MemberWrapper
        {
            public string type;
            public string json;

            public MemberWrapper(string type, string json){
                this.type = type;
                this.json = json;
            }
        }

        public abstract System.Object ToOriginalObject();

        public abstract void InitFromOriginalObejct(System.Object src);

        public string Encode() {
            return JsonUtility.ToJson(this,false);
        }
    }
}