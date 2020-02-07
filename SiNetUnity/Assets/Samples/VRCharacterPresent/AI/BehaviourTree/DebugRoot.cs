using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class DebugRoot : MonoBehaviour
    {
        public Node debugTarget;
        public bool autoStart = false;
        private IEnumerator Start()
        {
            yield return null;

            if (debugTarget && autoStart)
                debugTarget.ExecuteNode();
        }
    }
}