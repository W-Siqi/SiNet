using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class RunToTarget : WolfKingLeafNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            stateTable.animator.SetTrigger("run1");
            var trackInfo = stateTable.navigator.Chase(
                stateTable.wolfFightTarget.transform,
                stateTable.attribute.runningSpeed,
                stateTable.attribute.trunningAngleSpeed,
                stateTable.attribute.closeBodyDistance) ;
            while (!trackInfo.isFinished)
            {
                if (stateTable.isStunned)
                    break;

                yield return null;
            }

            stateTable.navigator.StopNavigating();
            stateTable.animator.SetTrigger("idle");
            yield return null;

            if (stateTable.isStunned)
                _state = State.fail;
            else
                _state = State.success;
        }
    }
}