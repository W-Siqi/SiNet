using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet
{
    public class SyncInt : SyncComponent
    {
        public int val = 0;

        public override SyncComponentSnapshot GetSnapshot()
        {
            var snapshot = new SyncComponentSnapshot();
            snapshot.innerID = innerID;
            snapshot.values = new float[] { val };
            return snapshot;
        }

        public override void SyncToSnapshot(SyncComponentSnapshot snapshot)
        {
            if (snapshot.values.Length != 1) {
                Debug.Log("[Bad message]-state error");
                return;
            }

            val = Mathf.FloorToInt(snapshot.values[0]);
        }
    }
}