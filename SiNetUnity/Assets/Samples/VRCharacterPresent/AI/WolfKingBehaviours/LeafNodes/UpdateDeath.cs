using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class UpdateDeath : WolfKingLeafNode
    {
        private bool hasExecuted = false;
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            yield return null;

            if (!hasExecuted) {
                hasExecuted = true;
                stateTable.animator.SetTrigger("stunned");
                WolfKingLevelRunner.instance.EndLevel();
                var sword = stateTable.swordConnecion.attachedSword;
                sword.FallFromMouth();
            }

            _state = State.success;
        }
    }
}