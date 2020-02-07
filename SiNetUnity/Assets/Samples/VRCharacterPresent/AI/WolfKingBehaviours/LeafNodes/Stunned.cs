using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class Stunned : WolfKingLeafNode
    {
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("stunned");

            stateTable.isVunlerable = true;
            var sword = stateTable.swordConnecion.attachedSword;
            sword.FallFromMouth();

            yield return new WaitForSeconds(stateTable.attribute.stunSustainTime);

            stateTable.stunProcess = 0;
            stateTable.isVunlerable = false;

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }
    }
}