using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public abstract class CompositeNode : Node
    {
        [SerializeField]
        protected Node[] childs;

        private void Start()
        {
            GetChildNodes();    
        }

        private void GetChildNodes() {
            var nodes = GetComponentsInChildren<Node>();
            var childList = new List<Node>();

            foreach (var n in nodes) {
                if (n.transform.parent == transform)
                    childList.Add(n);
            }

            childs = childList.ToArray();
        }
    }
}
