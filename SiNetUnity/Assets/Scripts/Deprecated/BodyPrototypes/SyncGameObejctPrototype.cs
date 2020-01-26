using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    public class SyncEntityPrototype:MessageBodyPrototype
    {   
        public string[] syncStateJSONs;
        public int sceneUID;
        public int mirrorObjectID = -1;

        public SyncEntityPrototype(SyncEntity syncEntity)
        {
            var jsonfiedStates = new List<string>();
            foreach (var state in syncEntity.syncStates) {
                jsonfiedStates.Add(JsonUtility.ToJson(state));
            }
            this.syncStateJSONs = jsonfiedStates.ToArray();

            this.sceneUID = syncEntity.sceneUID;
            this.mirrorObjectID = syncEntity.mirrorObjectID;
        }

        public SyncEntityPrototype() {
        }

        // unity's jsonUtility doesn't support serilzie array of object. so...
        public override string Encode()
        {
            return JsonUtility.ToJson(this);
        }

        public override void DecodeFrom(string body)
        {
            try
            {
                var decodeResult = JsonUtility.FromJson<SyncEntityPrototype>(body);
                this.sceneUID = decodeResult.sceneUID;
                this.mirrorObjectID = decodeResult.mirrorObjectID;
                this.syncStateJSONs = decodeResult.syncStateJSONs;
            }
            catch {
                throw;
            }
        }

        public override Object ToOriginal()
        {
            var original = new SyncEntity();

            original.sceneUID = this.sceneUID;

            original.mirrorObjectID = this.mirrorObjectID;

            var decodedStates = new List<SyncComponent>();
            foreach (var jsonStr in syncStateJSONs) {
                try
                {
                    var decoded = JsonUtility.FromJson<SyncComponent>(jsonStr);
                    decodedStates.Add(decoded);
                }
                catch {
                    throw;
                }
            }
            original.syncStates = decodedStates.ToArray();

            return original;
        }
    }
}