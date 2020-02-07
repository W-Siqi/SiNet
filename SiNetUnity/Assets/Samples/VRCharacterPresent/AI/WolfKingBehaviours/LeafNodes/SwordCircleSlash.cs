using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class SwordCircleSlash : WolfKingLeafNode{
        private const float CIRCLE_ROTATE_TIME = 0.3f;
        private const float ANIMATION_WAIT_LEFT = 0.65f;
        private const float ANIMATION_WAIT_RIGHT = 0.9f;

        [SerializeField]
        private float startLatency = 0.5f;
        [SerializeField]
        private bool isInstant = false;
        [SerializeField]
        private WolfKingAttackNodeAttribute atkAttribute;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var sword = stateTable.swordConnecion.attachedSword;

            // set animation and wait accroding to direction
            if (!isInstant) {
                if (sword.state == WolfSword.State.left) {
                    stateTable.animator.SetTrigger("slashFastRight");
                    yield return new WaitForSeconds(ANIMATION_WAIT_RIGHT);
                }
                else if (sword.state == WolfSword.State.right) {
                    stateTable.animator.SetTrigger("slashFastLeft");
                    yield return new WaitForSeconds(ANIMATION_WAIT_LEFT);
                }
                else               
                    Debug.LogError("SWORD: state wrong");            
            }

            // circle Attack
            StartCoroutine(ActivateAttack());

            // sword&body rotate
            if (sword.state == WolfSword.State.left)
            {
                sword.SlashToRight(CIRCLE_ROTATE_TIME);
                StartCoroutine(BodyRotate(1, CIRCLE_ROTATE_TIME));
            }
            else if (sword.state == WolfSword.State.right)
            {
                sword.SlashToLeft(CIRCLE_ROTATE_TIME);
                StartCoroutine(BodyRotate(-1, CIRCLE_ROTATE_TIME));
            }
            else
            {
                Debug.LogError("SWORD: state wrong");
            } 

            // wait sword finish
            while (sword.state == WolfSword.State.moving) {
                yield return null;
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        private IEnumerator ActivateAttack()
        {
            yield return null;
            if (state == State.running)
            {        
                var atk = WolfKingAttack.InstantiateAttack(stateTable.wolfKing,
                    stateTable.wolfKing.transform.position,
                    stateTable.attribute.wolfBodyAttackRadius,
                    atkAttribute.attackValue,
                    atkAttribute.defendable,
                    atkAttribute.force);

                // wait till end
                while (state == State.running)
                {
                    yield return null;
                }

                if(atk)
                    atk.FinishAttack();
            }
        }

        private IEnumerator BodyRotate(float directionFactor,float finishTime) {
            var startTime = Time.time;
            var rotateSpeed = 360 / finishTime;
            while (Time.time < startTime + finishTime)
            {
                stateTable.navigator.transform.Rotate(Vector3.up, directionFactor * rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
