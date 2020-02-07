using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    public class SwordSlash : WolfKingLeafNode{
        private const float SLASH_FINISH_TIME = 1.5f;

        private const float SLASH_LEFT_ATTACK_LATENCY = 0.65f;
        private const float SLASH_RIGHT_ATTACK_LATENCY = 0.9f;

        private const float SWORD_ROTATE_LATENCY = 0.5f;
        private const float SWORD_ROTATE_TIME = 0.3f;

        [SerializeField]
        private WolfKingAttackNodeAttribute atkAttribute;
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var endTime = Time.time + SLASH_FINISH_TIME;
            var sword = stateTable.swordConnecion.attachedSword;

            // animation and attack activate
            if (sword.state == WolfSword.State.left)
            {
                stateTable.animator.SetTrigger("slashFastRight");
                StartCoroutine(ActivateAttackLater(SLASH_RIGHT_ATTACK_LATENCY));
            }
            else {
                stateTable.animator.SetTrigger("slashFastLeft");
                StartCoroutine(ActivateAttackLater(SLASH_LEFT_ATTACK_LATENCY));
            }
         
            yield return new WaitForSeconds(SWORD_ROTATE_LATENCY);

            // sword rotate
            if (sword.state == WolfSword.State.left)
                sword.SlashToRight(SWORD_ROTATE_TIME);
            else if (sword.state == WolfSword.State.right)
                sword.SlashToLeft(SWORD_ROTATE_TIME);
            else
                Debug.LogError("State Error");

            // wait sword Finish
            while (true) {
                if (sword.state != WolfSword.State.moving && Time.time > endTime)
                    break;
                yield return null;
            }

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        private IEnumerator ActivateAttackLater(float seconds) {
            yield return new WaitForSeconds(seconds);
            if (state == State.running) {
                var sword = stateTable.swordConnecion.attachedSword;
                var atk = WolfKingAttack.InstantiateAttack(stateTable.wolfKing,
                    sword.transform.position,
                    stateTable.attribute.wolfBodyAttackRadius,
                    atkAttribute.attackValue,
                    atkAttribute.defendable,
                    atkAttribute.force);

                // wait till end
                while (state == State.running) {
                    yield return null;
                }

                if(atk)
                    atk.FinishAttack();
            }
        }
    }}