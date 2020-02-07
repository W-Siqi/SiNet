using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class Selector : CompositeNode
    {
        public enum Type {random,roundRobin}

        [SerializeField]
        private Type type;
        private int nextSelectIndex = 0;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var selectedIndex = type == Type.random ? Random.Range(0, childs.Length) : nextSelectIndex;

            nextSelectIndex = selectedIndex + 1;
            if (nextSelectIndex >= childs.Length)
                nextSelectIndex = 0;

            var selectedChild = childs[selectedIndex];
            selectedChild.ExecuteNode();
            while (true) {
                if (selectedChild.state != State.running) 
                    break;

                yield return null;
            }

            _state = selectedChild.state;
        }
    }
}