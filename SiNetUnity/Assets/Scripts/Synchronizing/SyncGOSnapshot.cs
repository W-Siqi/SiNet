using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class SyncGOSnapshot
    {
        public int sceneUID = -1;
        public int mirrorObjectID = -1;
        public SyncStateSnapshot[] syncStates;
    }
}