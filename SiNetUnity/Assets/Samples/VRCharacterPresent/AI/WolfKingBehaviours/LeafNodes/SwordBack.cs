using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    public class SwordBack : WolfKingLeafNode
    {
        private const float SWORD_BACK_ANIMATION_TIME = 0.5f;

        [SerializeField]
        private WolfKingAttackNodeAttribute atkAttribute;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            StartCoroutine(ActivateAttack());

            var sword = stateTable.swordConnecion.attachedSword;
            sword.BackToWolfMouth();
            while (true) {
                yield return null;
                if (sword.state != WolfSword.State.moving)
                    break;
            }

            stateTable.animator.SetTrigger("getSword");
            yield return new WaitForSeconds(SWORD_BACK_ANIMATION_TIME);
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
