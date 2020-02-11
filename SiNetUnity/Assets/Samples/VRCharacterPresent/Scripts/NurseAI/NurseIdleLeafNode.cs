using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class NurseIdleLeafNode :LeafNode
{
    protected override IEnumerator NodeRunning()
    {
        _state = State.running;

        var nurseBB = blackboard as SyncVRNurseBlackboard;

        // set animation
        nurseBB.animator.SetTrigger("idle");

        // wait till another antion break this one
        while (!IsIdleBreak(nurseBB))
        {
            yield return null;
        }

        if (IsIdleBreak(nurseBB))
            _state = State.fail;
        else
            _state = State.success;
    }

    private bool IsIdleBreak(SyncVRNurseBlackboard nurseBB) {
        return nurseBB.hasAction || nurseBB.isOverMovingSpeed;
    }
}
