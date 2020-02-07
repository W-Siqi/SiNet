using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class IceUltimateSkill : WolfKingLeafNode
    {
        [SerializeField]
        private float radius = 18f;
        [SerializeField]
        private int swordCount = 40;
        [SerializeField]
        private float sourceDepth = 20f;
        [SerializeField]
        private float shadowSwordAttackRadius = 7f;
        [SerializeField]
        private float shadowSwordDamage = 40f;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var wolfKingSword = stateTable.swordConnecion.attachedSword;
            stateTable.animator.SetTrigger("howling");

            var centerLaunchPos = stateTable.wolfKing.transform.position + sourceDepth*Vector3.down;
            // summon shaows
            var swordlist = new List<WolfSwordShadow>();
            for (int i = 0; i < swordCount; i++)
            {
                var randomPos = stateTable.wolfKing.transform.position;
                var randomBias = radius * Random.insideUnitSphere;
                randomBias.y = Mathf.Abs(randomBias.y);
                randomPos += randomBias;

                var direction = (randomPos - centerLaunchPos).normalized;

                var sword = WolfSwordShadow.SummonShaowAt(randomPos, direction, 100);
                AssignAttackToShdowSword(sword);
                swordlist.Add(sword);
            }

            // wait
            yield return new WaitForSeconds(3f);

            // shadow combine 
            foreach (var s in swordlist)
            {
                s.ShadowCombinedTo(wolfKingSword.transform);
            }

            stateTable.navigator.StopNavigating();
            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        private void AssignAttackToShdowSword(WolfSwordShadow shadowSword)
        {
            WolfKingAttack.InstantiateAttack(stateTable.wolfKing,
                shadowSword.transform.position,
                shadowSwordAttackRadius,
                shadowSwordDamage,
                true,
                0,
                shadowSword.transform);
        }
    }
}
