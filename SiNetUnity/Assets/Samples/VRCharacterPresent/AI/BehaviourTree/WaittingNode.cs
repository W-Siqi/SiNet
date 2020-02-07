using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class WaittingNode : LeafNode
    {
        [SerializeField]
        private float waitTime = 0.1f;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            yield return new WaitForSeconds(waitTime);
            _state = State.success;
        }
    }
}