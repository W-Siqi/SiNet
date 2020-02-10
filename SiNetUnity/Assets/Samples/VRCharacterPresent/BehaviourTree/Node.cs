using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public abstract class  Node : MonoBehaviour
    {
        public enum State {success,fail,running}

        public State state { get { return _state; } }

        protected State _state = State.success;
        
        public  void ExecuteNode() {
            StartCoroutine(NodeRunning());
        }

        protected abstract IEnumerator NodeRunning(); 
    }
}