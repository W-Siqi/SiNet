using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    // RPC is on the to-do list
    // the functions here are urgently, but we have a 'fake' RPC for now to make the framework run
    public class RPCPlaceHolder
    {
        public static int TempAllocateSceneUID() {
            return Random.Range(1, 999999);
        }
    }
}
