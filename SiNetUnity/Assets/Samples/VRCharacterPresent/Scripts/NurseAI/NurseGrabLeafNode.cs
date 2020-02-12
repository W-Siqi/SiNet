using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class NurseGrabLeafNode : LeafNode
{
    protected override IEnumerator NodeRunning()
    {
        _state = State.running;

        var nurseBB = blackboard as SyncVRNurseBlackboard;
        
        // set animation
        nurseBB.animator.SetTrigger("grab");
        nurseBB.grabActionTrigger = false;

        // wait till animation end or has another antion break this one
        const float ASSUME_GRAB_TIME = 2f;
        var endTime = Time.time + ASSUME_GRAB_TIME;
        while (Time.time <endTime) {
            yield return null;
            if (nurseBB.hasAction)
                break;
        }

        // attached the bottle
        var bottle = IVBottle.FindIVBottle(nurseBB.grabIVBottleID);
        if (bottle && nurseBB.handAnchorPoint) {
            bottle.isOnGrab = true;
            bottle.transform.SetParent(transform);
            bottle.transform.localPosition = Vector3.zero;
        }

        if (nurseBB.hasAction)
            _state = State.fail;
        else
            _state = State.success;
    }
}
