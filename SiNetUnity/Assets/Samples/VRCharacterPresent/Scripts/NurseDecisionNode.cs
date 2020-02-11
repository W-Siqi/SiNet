using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class NurseDecisionNode : Node
{
    [SerializeField]
    private Node actionRoot;
    [SerializeField]
    private Node moveRoot;
    [SerializeField]
    private Node idleRoot;

    protected override IEnumerator NodeRunning()
    {
        _state = State.running;

        // make decision here
        var characterBB = blackboard as SyncVRCharacterBlackboard;
        var choosedNode = idleRoot;
        if (characterBB.hasAction) {
            choosedNode = actionRoot;
        }
        else if (characterBB.isOverMovingSpeed) {
            choosedNode = moveRoot;
        }

        // run choosed node
        choosedNode.ExecuteNode();
        while (choosedNode.state == State.running) {
            yield return null;
        }

        _state = choosedNode.state;
    }
}
