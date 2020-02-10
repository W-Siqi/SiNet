using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class SyncTransform : SyncComponent
    {
        public Vector3 position;
        public Vector3 localScale;
        public Quaternion rotation;

        // sync to the real transform if it under the local authority
        protected override void Update()
        {
            base.Update();
            if (attachedEntity.authorityType == SyncEntity.AuthorityType.local) {
                position = transform.position;
                localScale = transform.localScale;
                rotation = transform.rotation;
            }
        }

        public override SyncComponentSnapshot GetSnapshot()
        {
            var snapshot = new SyncComponentSnapshot();
            snapshot.innerID = innerID;
            snapshot.timeStamp = timeStamp;

            // encode part
            snapshot.values = new float[]{
                position.x,position.y,position.z,
                localScale.x,localScale.y,localScale.z,
                rotation.x,rotation.y,rotation.z,rotation.w};

            return snapshot;
        }

        public override void SyncToSnapshot(SyncComponentSnapshot snapshot)
        {
            if (snapshot.values.Length != 10)
            {
                Debug.Log("[Bad message]-state error");
                return;
            }

            timeStamp = snapshot.timeStamp;

            // decode part
            var v = snapshot.values;
            position.Set(v[0], v[1], v[2]);
            localScale.Set(v[3], v[4], v[5]);
            rotation.Set(v[6], v[7], v[8], v[9]);
        }
    }
}
