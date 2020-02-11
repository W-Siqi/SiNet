using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    // ATTENTION: The order in syncStates matters, because we assign ID in this order.
    // So as mirror entity, the order MUST be same with original entity
    public class SyncEntity : MonoBehaviour
    {
        public enum AuthorityType {
            local,remote,server
        }

        public AuthorityType authorityType = AuthorityType.local;
        public int sceneUID = -1;
        public int mirrorObjectID = -1;
        public SyncComponent[] syncStates;

        // Start is called before the first frame update
        void Start()
        {
            int id = 1;
            foreach (var s in syncStates) {
                s.innerID = id++;
            }

            var checkSyncStates = GetComponentsInChildren<SyncComponent>();
            if (checkSyncStates.Length != syncStates.Length) {
                Debug.LogError("[SiNet]: the sync componet doesn't match! CHECK THE PREFAB");
            }
        }

        public SyncEntitySnapshot GetSnapshot() {
            var snapshot = new SyncEntitySnapshot();

            snapshot.timeStamp = ServerTime.current;
            snapshot.sceneUID = sceneUID;
            snapshot.mirrorObjectID = mirrorObjectID;

            var componentSnapshots = new List<SyncComponentSnapshot>();
            foreach (var state in syncStates)
                componentSnapshots.Add(state.GetSnapshot());
            snapshot.syncStates = componentSnapshots.ToArray();

            return snapshot;
        }

        public void SyncToSnapshot(SyncEntitySnapshot snapshot) {
            if (snapshot.sceneUID != sceneUID) {
                Debug.LogWarning("[Sync Fail]");
                return;
            }

            foreach (var stateSnapshot in snapshot.syncStates) {
                foreach (var state in syncStates) {
                    if (state.innerID == stateSnapshot.innerID)
                        state.SyncToSnapshot(stateSnapshot);
                }
            }        
        }
    }
}