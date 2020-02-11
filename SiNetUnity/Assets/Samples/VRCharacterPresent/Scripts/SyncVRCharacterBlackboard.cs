using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;
using BehaviourTree;

public class SyncVRCharacterBlackboard : Blackboard
{
    [SerializeField]
    SyncTransform headTransform;

    public bool hasAction = false;
    public bool isOverMovingSpeed = false;
    private void Update()
    {
        // update blackboard's variables   
    }
}
