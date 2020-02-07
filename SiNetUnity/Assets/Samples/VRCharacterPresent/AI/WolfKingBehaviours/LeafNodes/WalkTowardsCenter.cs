using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    public class WalkTowardsCenter : WolfKingLeafNode
    {
        [SerializeField]
        private float sustainTime = 4f;
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("walk");

            var trackingInfo = stateTable.navigator.GoToAreaCenter(
                stateTable.attribute.idleMoveSpeed,
                stateTable.attribute.trunningAngleSpeed);

            var endTime = Time.time + sustainTime;

            while (!trackingInfo.isFinished && Time.time <endTime)
                yield return null;

            stateTable.navigator.StopNavigating();
            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }
    }
}