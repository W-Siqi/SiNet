using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class Standup : WolfKingLeafNode
    {
        private const float STANDUP_TIME = 3f;
        protected override IEnumerator NodeRunning()
        {
            _state = State.running;
            stateTable.animator.SetTrigger("standUp");

            yield return new WaitForSeconds(STANDUP_TIME);

            stateTable.animator.SetTrigger("idle");
            yield return null;

            _state = State.success;
        }
    }
}
