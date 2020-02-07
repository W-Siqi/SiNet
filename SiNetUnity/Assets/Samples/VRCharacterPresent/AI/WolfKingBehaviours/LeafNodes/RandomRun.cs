using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class RandomRun : WolfKingLeafNode
    {
        [SerializeField]
        private Vector2 runingTimeRange = new Vector2(10, 20);
        [SerializeField]
        private Vector2 destinationSwithTimeRange = new Vector2(1f, 5f);
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var endTime = Time.time + Random.Range(runingTimeRange.x, runingTimeRange.y);
            stateTable.animator.SetTrigger("run1");
            while (Time.time < endTime) {
                // start a random pos
                var timeForThisPos = Random.Range(destinationSwithTimeRange.x, destinationSwithTimeRange.y);
                var endTimeForThisPos = Time.time + timeForThisPos;
                endTimeForThisPos = Mathf.Min(endTimeForThisPos, endTime);
                var trackingInfo = stateTable.navigator.GoToRandomPosition(stateTable.attribute.runningSpeed,stateTable.attribute.trunningAngleSpeed);
                while (Time.time < endTimeForThisPos && !trackingInfo.isFinished) {
                    yield return null;
                }
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;
            _state = State.success;
        }
    }
}
