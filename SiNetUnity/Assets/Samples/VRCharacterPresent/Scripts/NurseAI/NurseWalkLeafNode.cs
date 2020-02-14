using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class NurseWalkLeafNode : LeafNode
{
    protected override IEnumerator NodeRunning()
    {
        _state = State.running;

        var nurseBB = blackboard as SyncVRNurseBlackboard;

        // set animation
        nurseBB.animator.SetTrigger("walk");

        while (!IsWalkBreak(nurseBB))
        {
            yield return null;
        }

        nurseBB.animator.SetTrigger("idle");
        if (IsWalkBreak(nurseBB))
            _state = State.fail;
        else
            _state = State.success;
    }

    private bool IsWalkBreak(SyncVRNurseBlackboard nurseBB)
    {
        return nurseBB.hasAction || !nurseBB.isOverMovingSpeed;
    }
}
