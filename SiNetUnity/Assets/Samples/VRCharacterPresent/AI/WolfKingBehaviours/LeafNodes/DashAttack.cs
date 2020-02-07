using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class DashAttack : WolfKingLeafNode
    {
        [SerializeField]
        private WolfKingAttackNodeAttribute atkAttribute;
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("dash");

            StartCoroutine(ActivateAttackLater(stateTable.attribute.dashAttackLatency));

            var trackingInfo = stateTable.navigator.Dash(stateTable.attribute.dashSpeed, stateTable.attribute.dashDistance);
            while (true) {
                if (trackingInfo.isFinished)
                    break;
                yield return null;
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        private IEnumerator ActivateAttackLater(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (state == State.running)
            {
                var atk = WolfKingAttack.InstantiateAttack(stateTable.wolfKing,
                    stateTable.wolfKing.transform.position,
                    stateTable.attribute.wolfBodyAttackRadius,
                    atkAttribute.attackValue,
                    atkAttribute.defendable,
                    atkAttribute.force,
                    stateTable.wolfKing.transform);

                // wait till end
                while (state == State.running)
                {
                    yield return null;
                }

                if (atk)
                    atk.FinishAttack();
            }
        }
    }
}
