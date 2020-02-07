using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    public class BackAndForth : WolfKingLeafNode{
        [SerializeField]
        private Vector2 timeRange = new Vector2(2, 6);

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("walk");

            stateTable.navigator.BackAndForth(stateTable.attribute.idleMoveSpeed);

            var sustainingTime = Random.Range(timeRange.x, timeRange.y);
            var endTime = Time.time + sustainingTime;
            while (Time.time < endTime)
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
    }}