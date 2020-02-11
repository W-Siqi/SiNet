using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    // this need 1 frame to init (in the start).
    // so its frame wouln't be ready after the first frame

    // TBD: use hash map to store the sync States
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
            syncStates = GetComponentsInChildren<SyncComponent>();
            int id = 1;
            foreach (var s in syncStates) {
                s.innerID = id++;
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