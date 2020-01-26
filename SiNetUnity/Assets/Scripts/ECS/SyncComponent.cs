using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    [RequireComponent(typeof(SyncEntity))]
    public abstract class SyncComponent : MonoBehaviour
    {
        public int innerID = -1;

        public abstract SyncComponentSnapshot GetSnapshot();

        public abstract void SyncToSnapshot(SyncComponentSnapshot snapshot);
    }
}