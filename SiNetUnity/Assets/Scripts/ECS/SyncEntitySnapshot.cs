using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class SyncEntitySnapshot
    {
        public int sceneUID = -1;
        public int mirrorObjectID = -1;
        public SyncComponentSnapshot[] syncStates;
    }
}