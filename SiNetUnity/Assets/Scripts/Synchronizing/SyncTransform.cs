using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class SyncTransform : SyncState
    {
        public Vector3 position;
        public Vector3 localScale;
        public Quaternion rotation;

        private SyncGameObject attachedEntity = null;

        private void Start()
        {
            attachedEntity = GetComponent<SyncGameObject>();
       
        }

        // sync to the real transform if it under the local authority
        private void Update()
        {
            if (attachedEntity.authorityType == SyncGameObject.AuthorityType.local) {
                position = transform.position;
                localScale = transform.localScale;
                rotation = transform.rotation;
            }
        }

        public override SyncStateSnapshot GetSnapshot()
        {
            var snapshot = new SyncStateSnapshot();
            snapshot.innerID = innerID;

            // encode part
            snapshot.values = new float[]{
                position.x,position.y,position.z,
                localScale.x,localScale.y,localScale.z,
                rotation.x,rotation.y,rotation.z,rotation.w};

            return snapshot;
        }

        public override void SyncToSnapshot(SyncStateSnapshot snapshot)
        {
            if (snapshot.values.Length != 10)
            {
                Debug.Log("[Bad message]-state error");
                return;
            }

            // decode part
            var v = snapshot.values;
            position.Set(v[0], v[1], v[2]);
            localScale.Set(v[3], v[4], v[5]);
            rotation.Set(v[6], v[7], v[8], v[9]);
        }
    }
}
