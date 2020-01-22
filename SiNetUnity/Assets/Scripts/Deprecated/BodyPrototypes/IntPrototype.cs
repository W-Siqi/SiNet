using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class IntPrototype : MessageBodyPrototype
    {
        [SerializeField]
        private string hint;
        [SerializeField]
        private int[] vals;

        public IntPrototype(string hint, int val) {
            this.hint = hint;
            this.vals = new int[] {val};
        }

        public IntPrototype(string hint, int[] vals)
        {
            this.hint = hint;
            this.vals = vals;
        }

        public override string Encode()
        {
            var d = JsonUtility.ToJson(this);
            return JsonUtility.ToJson(this);
        }

        public override void DecodeFrom(string body)
        {
            throw new System.NotImplementedException();
        }

        public override Object ToOriginal()
        {
            throw new System.NotImplementedException();
        }
    }
}