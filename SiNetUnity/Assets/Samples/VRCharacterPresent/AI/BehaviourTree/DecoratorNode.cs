using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public abstract class DecoratorNode : Node{
        protected Node decorateTarget;

        private void Start()
        {
            FindDecorateTargetInChildren();    
        }

        void FindDecorateTargetInChildren() {
            var nodes = GetComponentsInChildren<Node>();

            var findOne = false;
            foreach (var n in nodes)
            {
                if (n.transform.parent == transform) {
                    decorateTarget = n;
                    findOne = true;
                    break;
                }
            }

            if (!findOne)
                Debug.LogError("fail to find target");
        }  
    }
}