using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public abstract class Node : MonoBehaviour
    {
        public enum State { success, fail, running }

        public State state { get { return _state; } }

        protected State _state = State.success;

        protected Blackboard blackboard;

        public void ExecuteNode() {
            StartCoroutine(NodeRunning());
        }

        public void SetBlackboard(Blackboard blackboard) {
            if (this.blackboard)
            {
                Debug.LogError("[BT] set black board failed , because it already has one");
            }
            else {
                this.blackboard = blackboard;
            }
        }

        protected abstract IEnumerator NodeRunning(); 
    }
}