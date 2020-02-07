using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    public class StunedConditionDecorator :WolfKingConditionNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            if (stateTable.isStunned)
            {
                decorateTarget.ExecuteNode();
                while (true)
                {
                    if (decorateTarget.state != State.running)
                        break;
                    yield return null;
                }
                _state = decorateTarget.state;
            }
            else {
                _state = State.success;
            }
        }
    }
}