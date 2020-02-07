using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class HPConditonDecorator : WolfKingConditionNode
    {
        [SerializeField]
        private float minActivateHPValue = 0;
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            if (stateTable.currentHP < minActivateHPValue)
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
            else
            {
                _state = State.success;
            }
        }
    }
}