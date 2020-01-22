using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    // this need 1 frame to init (in the start).
    // so its frame wouln't be ready after the first frame

    // TBD: use hash map to store the sync States
    public class SyncGameObject : MonoBehaviour
    {
        public enum AuthorityType {
            local,remote,server
        }

        public AuthorityType authorityType = AuthorityType.local;
        public int sceneUID = -1;
        public int mirrorObjectID = -1;
        public SyncState[] syncStates;

        // Start is called before the first frame update
        void Start()
        {
            syncStates = GetComponentsInChildren<SyncState>();
            int id = 1;
            foreach (var s in syncStates) {
                s.innerID = id++;
            }
        }

        public SyncGOSnapshot GetSnapshot() {
            var snapshot = new SyncGOSnapshot();

            snapshot.sceneUID = sceneUID;
            snapshot.mirrorObjectID = mirrorObjectID;

            var stateSnapshots = new List<SyncStateSnapshot>();
            foreach (var state in syncStates)
                stateSnapshots.Add(state.GetSnapshot());
            snapshot.syncStates = stateSnapshots.ToArray();

            return snapshot;
        }

        public void SyncToSnapshot(SyncGOSnapshot snapshot) {
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