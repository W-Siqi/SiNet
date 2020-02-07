using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class RunToCornor : WolfKingLeafNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("run1");

            var speed = stateTable.attribute.runningSpeed;
            var angleSpeed = stateTable.attribute.trunningAngleSpeed;
            var trackingInfo = stateTable.navigator.ToCornor(speed, angleSpeed);
            while (!trackingInfo.isFinished) {
                yield return null;
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }
    }

}