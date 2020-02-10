using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    [RequireComponent(typeof(SyncEntity))]
    public abstract class SyncComponent : MonoBehaviour
    {
        public float timeStamp;

        public int innerID = -1;

        public abstract SyncComponentSnapshot GetSnapshot();

        public abstract void SyncToSnapshot(SyncComponentSnapshot snapshot);

        protected SyncEntity attachedEntity = null;

        virtual protected void Start()
        {
            attachedEntity = GetComponent<SyncEntity>();

        }

        virtual protected void Update() {
            if (attachedEntity.authorityType == SyncEntity.AuthorityType.local) {
                // sync to server time
            }
        }
    }
}