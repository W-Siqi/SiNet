using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class TransmitableRPCPlaceHolderInfo : TransmitableObject
    {
        public string name = "";

        public override void InitFromOriginalObejct(object src)
        {
            if (src is RPCSignaturePlaceHolder) {
                var signature = src as RPCSignaturePlaceHolder;
                name = signature.name;
            }
        }

        public override object ToOriginalObject()
        {
            var signature = new RPCSignaturePlaceHolder(name);
            return signature;
        }
    }
}