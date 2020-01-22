using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    [RequireComponent(typeof(SyncGameObject))]
    public abstract class SyncState : MonoBehaviour
    {
        public int innerID = -1;

        public abstract SyncStateSnapshot GetSnapshot();

        public abstract void SyncToSnapshot(SyncStateSnapshot snapshot);
    }
}