using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet{
    // original type is 
    public class TransmitableSyncGOSnapshot : TransmitableObject
    {
        public int sceneUID;
        public int mirrorObjectID = -1;
        public List<string> encodedMembers = new List<string>();

        public override void InitFromOriginalObejct(System.Object src)
        {
            if (src is SyncEntitySnapshot)
            {
                var syncGO = src as SyncEntitySnapshot;

                this.sceneUID = syncGO.sceneUID;
                this.mirrorObjectID = syncGO.mirrorObjectID;

                foreach (var state in syncGO.syncStates) {
                    var wrapper = new MemberWrapper("SyncStateSnapshot", JsonUtility.ToJson(state,false));
                    encodedMembers.Add(JsonUtility.ToJson(wrapper,false));
                }
            }
            else {
                throw new System.Exception("type convert fail");
            }
        }

        public override System.Object ToOriginalObject()
        {
            var snapShot = new SyncEntitySnapshot();
            snapShot.sceneUID = sceneUID;
            snapShot.mirrorObjectID = mirrorObjectID;

            var syncStates = new List<SyncComponentSnapshot>();
            foreach (var encodedState in encodedMembers) {
                try {
                    var memeberWraper = JsonUtility.FromJson<MemberWrapper>(encodedState);

                    if (memeberWraper.type != "SyncStateSnapshot")
                        throw new System.Exception("bad json");

                    syncStates.Add(JsonUtility.FromJson<SyncComponentSnapshot>(memeberWraper.json));
                }
                catch {
                    throw;
                }
            }

            snapShot.syncStates = syncStates.ToArray();

            return snapShot;
        }
    }
}