using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    public class NetworkConfig
    {
        public string hostIP = "127.0.0.1";
        public int port;
        public SpawnTable spawnTable;
        public float syncFrames;
        public float maxWaitTime;

        public class SpawnTable
        {
            public class SpawnItem
            {
                public string nameID;
                public GameObject prefab;
            }
        }
    }
}