using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class RootNode : LoopDecorator
    {
        [SerializeField]
        private Blackboard blackboardOfRoot;

        protected override IEnumerator NodeRunning()
        {
    
            yield return null;

            // init blackboard
            var childNodes = GetComponentsInChildren<Node>();
            foreach (var child in childNodes) {
                child.SetBlackboard(blackboardOfRoot);
            }

            // loop the child node
            StartCoroutine(base.NodeRunning());
        }
    }
}
