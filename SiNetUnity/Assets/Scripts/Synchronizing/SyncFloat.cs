using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet
{
    public class SyncFloat : SyncState
    {
        public float val = 0;

        public override SyncStateSnapshot GetSnapshot()
        {
            var snapshot = new SyncStateSnapshot();
            snapshot.innerID = innerID;
            snapshot.values = new float[] { val };
            return snapshot;
        }

        public override void SyncToSnapshot(SyncStateSnapshot snapshot)
        {
            if (snapshot.values.Length != 1) {
                Debug.Log("[Bad message]-state error");
                return;
            }

            val = snapshot.values[0];
        }
    }
}