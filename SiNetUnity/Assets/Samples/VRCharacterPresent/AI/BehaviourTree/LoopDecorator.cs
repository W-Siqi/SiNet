using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree
{
    public class LoopDecorator : DecoratorNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            while (true) {
                decorateTarget.ExecuteNode();

                while (true) {
                    yield return null;
                    if (decorateTarget.state != State.running)
                        break;
                }
            }
        }
    }
}