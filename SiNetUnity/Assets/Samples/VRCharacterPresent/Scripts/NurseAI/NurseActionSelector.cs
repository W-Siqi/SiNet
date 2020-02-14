using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class NurseActionSelector : Node
{
    [SerializeField]
    private NurseGrabLeafNode grabNode;
    [SerializeField]
    private NurseOpenWindowLeafNode openWindow;

    protected override IEnumerator NodeRunning()
    {
        _state = State.running;

        var nurseBB = blackboard as SyncVRNurseBlackboard;

        // lock the movement sync
        nurseBB.movementDecorator.applyMovementSystem = false;

        Node choosedNode = null;
        if (nurseBB.grabActionTrigger)
        {
            choosedNode = grabNode;
        }
        else if (nurseBB.openWindowTrigger) {
            choosedNode = openWindow;
        }

        if (choosedNode)
        {
            choosedNode.ExecuteNode();
            while (choosedNode.state == State.running) {
                yield return null;
            }
            _state = choosedNode.state;
        }
        else {
            _state = State.fail;
        }

        // recover movement sync
        nurseBB.movementDecorator.applyMovementSystem = true;
    }
}
