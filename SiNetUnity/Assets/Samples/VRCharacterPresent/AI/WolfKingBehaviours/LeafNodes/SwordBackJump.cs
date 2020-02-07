using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class SwordBackJump : WolfKingLeafNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("jump");

            var sword = stateTable.swordConnecion.attachedSword;
            sword.BackToWolfMouth();

            var attribute = stateTable.attribute;
            var trackingInfo = stateTable.navigator.Jump(attribute.jumpHorizonSpeed, attribute.jumpVerticalSpeed);
            while (true) {
                if (trackingInfo.isFinished)
                    break;

                yield return null;
            }

            while (true) {
                if (sword.state != WolfSword.State.moving)
                    break;

                yield return null;
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }
    }
}