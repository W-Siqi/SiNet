using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class FacingTarget : WolfKingLeafNode{
        [SerializeField]
        private Vector2 timeRange = new Vector2(2,6);

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            stateTable.animator.SetTrigger("walk");

            stateTable.soundPlayer.PlaySound(stateTable.soundPlayer.growl);

            stateTable.navigator.Facing(stateTable.wolfFightTarget.transform,
                stateTable.attribute.idleMoveSpeed,
                stateTable.attribute.trunningAngleSpeed,
                stateTable.attribute.facingDistance);

            var sustainingTime = Random.Range(timeRange.x, timeRange.y);
            var endTime = Time.time + sustainingTime;
            while (true) {
                if (Time.time >= endTime || stateTable.isStunned) {
                    stateTable.navigator.StopNavigating();
                    break;
                }
                yield return null;
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;

            if (stateTable.isStunned)
                _state = State.fail;
            else
                _state = State.success;
        }
    }
}
