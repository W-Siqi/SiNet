using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class SwordThrow : WolfKingLeafNode
    {
        [SerializeField]
        private WolfKingAttackNodeAttribute atkAttribute;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var sword = stateTable.swordConnecion.attachedSword;
            // turn a circle
            var startTime = Time.time;
            var finishTime = 0.3f;
            var rotateSpeed = 360 / finishTime;
            var rotateDirectionFactor = sword.state == WolfSword.State.left ? 1f : -1f;

            while (Time.time < startTime + finishTime)
            {
                stateTable.navigator.transform.Rotate(Vector3.up, rotateDirectionFactor * rotateSpeed * Time.deltaTime);
                yield return null;
            }

            stateTable.animator.SetTrigger("walk");

            // throw sword- stage1- to high pos
            float height = 15f;
            sword.FlyTo(stateTable.wolfKing.transform.position + Vector3.up * height, Quaternion.Euler(-60, -160, 260));
            StartCoroutine(ActivateAttack());
            stateTable.navigator.BackAndForth(stateTable.attribute.idleMoveSpeed);

            while (true) {
                if (sword.state != WolfSword.State.moving)
                    break;
                yield return null;
            }

            // throw sword- stage2- to real pos
            sword.FlyTo(stateTable.wolfFightTarget.transform.position);
            StartCoroutine(ActivateAttack());
            stateTable.navigator.BackAndForth(stateTable.attribute.idleMoveSpeed);

            while (true)
            {
                if (sword.state != WolfSword.State.moving)
                    break;
                yield return null;
            }

            // sword hit target
            stateTable.navigator.StopNavigating();
            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        private IEnumerator ActivateAttack()
        {
            if (state == State.running)
            {
                var sword = stateTable.swordConnecion.attachedSword;

                var atk = WolfKingAttack.InstantiateAttack(stateTable.wolfKing,
                    sword.transform.position,
                    stateTable.attribute.swordAttackRadius,
                    atkAttribute.attackValue,
                    atkAttribute.defendable,
                    atkAttribute.force,
                    sword.transform);

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