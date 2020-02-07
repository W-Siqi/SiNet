using System.Collections;
using System.Collections.Generic;
using UnityEngine;namespace BehaviourTree {
    public class CircleRun : WolfKingLeafNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var trackInfo = stateTable.navigator.RunRouteFromStartToEnd(
                stateTable.circleRoute, 
                stateTable.attribute.runningSpeed,
                stateTable.attribute.trunningAngleSpeed);

            while (!trackInfo.isFinished)
            {
                if (stateTable.isStunned) {
                    stateTable.navigator.StopNavigating();
                    break;
                }
                yield return null;
            }

            if (stateTable.isStunned)
                _state = State.fail;
            else
                _state = State.success;
        }
    }}
