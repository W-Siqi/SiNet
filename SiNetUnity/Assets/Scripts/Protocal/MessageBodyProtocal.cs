using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the encoder should be extend as we need more type of body
namespace SiNet {
    public class MessageBodyProtocal
    {
        public static string EncodeSyncGameObject(SyncGameObject src) {
            var syncBody = new SyncGameObjectPrototype(src);
            return syncBody.Encode();
        }

        public static SyncGameObjectPrototype DecodeSyncGameObject(string body) {
            var prototype = new SyncGameObjectPrototype();
            try
            {
                prototype.DecodeFrom(body);
            }
            catch {
                throw;
            }

            return prototype;
        }

        public static string EncodeInt(string hint,int val) {
            var intBody = new IntPrototype(hint, val);
            return intBody.Encode();
;       }
    }
}
