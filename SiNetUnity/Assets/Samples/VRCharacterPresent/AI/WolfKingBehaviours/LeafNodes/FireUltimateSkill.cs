using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class FireUltimateSkill : WolfKingLeafNode
    {
        const float ATK_INTERVAL = 0.15f;
        const float COMBINE_INTERVAL = 0.08f;
        const float KING_SWORD_FLYHEIGHT = 100f;

        [SerializeField]
        private float radius = 18f;
        [SerializeField]
        private int swordCount = 40;
        [SerializeField]
        private float shadowSwordAttackRadius = 7f;
        [SerializeField]
        private float shadowSwordDamage = 40f;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            var wolfKingSword = stateTable.swordConnecion.attachedSword;

            stateTable.soundPlayer.PlaySound(stateTable.soundPlayer.howling);

            // throw wolkKingSword to sky
            wolfKingSword.FlyTo(stateTable.swordConnecion.ultimateSkillAnchor.position + Vector3.up * KING_SWORD_FLYHEIGHT);
            stateTable.animator.SetTrigger("howling");
            while (wolfKingSword.state == WolfSword.State.moving)
                yield return null;

            // to anchor
            wolfKingSword.FlyTo(
                stateTable.swordConnecion.ultimateSkillAnchor.position,
                stateTable.swordConnecion.ultimateSkillAnchor.rotation);
            while (wolfKingSword.state == WolfSword.State.moving)
                yield return null;

            stateTable.navigator.StopNavigating();
            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        IEnumerator WithSwordRain() {
            _state = State.running;

            var wolfKingSword = stateTable.swordConnecion.attachedSword;
            // throw wolkKingSword
            wolfKingSword.FlyTo(stateTable.swordConnecion.ultimateSkillAnchor.position + Vector3.up * KING_SWORD_FLYHEIGHT);
            stateTable.animator.SetTrigger("howling");

            // summon shaows
            var swordlist = new List<WolfSwordShadow>();
            for (int i = 0; i < swordCount; i++)
            {
                var randomPos = stateTable.wolfKing.transform.position;
                randomPos += radius * Random.insideUnitSphere;
                randomPos.y = stateTable.wolfKing.transform.position.y + 1f;

                var randomDirection = Vector3.down * 3f + Random.insideUnitSphere;
                randomDirection.Normalize();

                var sword = WolfSwordShadow.SummonShaowAt(randomPos, randomDirection);
                AssignAttackToShdowSword(sword);
                swordlist.Add(sword);

                yield return new WaitForSeconds(ATK_INTERVAL);
            }

            // wolfKing To anchor
            while (wolfKingSword.state == WolfSword.State.moving)
                yield return null;

            wolfKingSword.FlyTo(
                stateTable.swordConnecion.ultimateSkillAnchor.position,
                stateTable.swordConnecion.ultimateSkillAnchor.rotation);

            while (wolfKingSword.state == WolfSword.State.moving)
                yield return null;

            // shadow combine 
            foreach (var s in swordlist)
            {
                s.ShadowCombinedTo(wolfKingSword.transform);
                yield return new WaitForSeconds(COMBINE_INTERVAL);
            }

            // final attack
            stateTable.navigator.StopNavigating();
            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }

        private void AssignAttackToShdowSword(WolfSwordShadow shadowSword) {
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

