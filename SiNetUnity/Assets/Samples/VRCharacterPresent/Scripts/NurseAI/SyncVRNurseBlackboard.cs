using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;
using BehaviourTree;

public class SyncVRNurseBlackboard : Blackboard
{
    const float MIN_MOVE_SPEED = 0.3f;

    [SerializeField]
    Transform bodyTransform;
    [SerializeField]
    SyncInt pickupTriggerCounter;

    public bool isOverMovingSpeed = false;
    public bool grabActionTrigger = false;
    public Animator animator;
    public Vector3 bodyVelocity = Vector3.zero;

    public bool hasAction {
        get {
            return grabActionTrigger;
        }
    }

    private int previousPickupTriggerCounter = 0;
    private Vector3 lastPos;

    private void Start()
    {
        lastPos = bodyTransform.position;
    }

    private void Update()
    {
        // update velocity
        var curPos = bodyTransform.position;
        bodyVelocity = (curPos - lastPos) / Time.deltaTime;
        lastPos = curPos;

        isOverMovingSpeed = bodyVelocity.magnitude > MIN_MOVE_SPEED;

        if (pickupTriggerCounter.val != previousPickupTriggerCounter) {
            previousPickupTriggerCounter = pickupTriggerCounter.val;
            grabActionTrigger = true; 
        }
    }
}
