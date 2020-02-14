using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class NurseOpenWindowLeafNode : LeafNode
{
    protected override IEnumerator NodeRunning()
    {
        _state = State.running;

        var nurseBB = blackboard as SyncVRNurseBlackboard;

        // lock the movement sync
        nurseBB.movementDecorator.applyMovementSystem = false;

        // set animation
        nurseBB.animator.SetTrigger("openWindow");
        nurseBB.openWindowTrigger = false;

        // wait till animation end or has another antion break this one
        const float ASSUME_TIME = 2f;
        var endTime = Time.time + ASSUME_TIME;
        while (Time.time <endTime) {
            yield return null;
            if (nurseBB.hasAction)
                break;
        }

        // recover movement sync
        nurseBB.movementDecorator.applyMovementSystem = true;

        if (nurseBB.hasAction)
            _state = State.fail;
        else
            _state = State.success;
    }
}
