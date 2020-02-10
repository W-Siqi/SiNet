using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : CompositeNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            bool hasChildFailed = false;
            foreach (var child in childs) {
                child.ExecuteNode();

                while (true) {
                    if (child.state == State.success)
                    {
                        break;
                    }
                    else if (child.state == State.fail) { 
                        hasChildFailed = true;
                        break;
                    }
                    yield return null;
                }

                if (hasChildFailed)
                    break;
            }

            if (hasChildFailed)
                _state = State.fail;
            else
                _state = State.success;
        }
    }
}