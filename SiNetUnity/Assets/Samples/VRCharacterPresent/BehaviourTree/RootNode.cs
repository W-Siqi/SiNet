using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class RootNode : LoopDecorator
    {
        [SerializeField]
        private Blackboard blackboardOfRoot;
        [SerializeField]
        private bool autoStart = false;

        protected override void Start()
        {
            base.Start();

            // init blackboard
            var childNodes = GetComponentsInChildren<Node>();
            foreach (var child in childNodes)
            {
                child.SetBlackboard(blackboardOfRoot);
            }

            // auto start check
            if (autoStart) {
                ExecuteNode();
            }
        }

        protected override IEnumerator NodeRunning()
        {
    
            yield return null;
            // loop the child node
            StartCoroutine(base.NodeRunning());
        }
    }
}
