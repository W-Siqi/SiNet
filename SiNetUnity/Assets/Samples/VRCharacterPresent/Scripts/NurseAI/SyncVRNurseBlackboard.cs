using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;
using BehaviourTree;

public class SyncVRNurseBlackboard : Blackboard
{
    const float MIN_MOVE_SPEED = 0.3f;
    const float VELOCITY_MAX_SUSTAIN_INTERVAL = 0.5f;

    [SerializeField]
    Transform bodyTransform;
    [SerializeField]
    SyncInt pickupTriggerCounter;

    public bool isOverMovingSpeed = false;
    public bool grabActionTrigger = false;
    public Animator animator;
    public Vector3 bodyVelocity = Vector3.zero;
    public int grabIVBottleID = -1;

    public bool hasAction {
        get {
            return grabActionTrigger;
        }
    }

    private int previousPickupTriggerCounter = 0;
    private Vector3 lastPos;
    private float lastVelocityUpdateTime = -1;

    private void Start()
    {
        lastPos = bodyTransform.position;
    }

    private void Update()
    {
        // update velocity
        var curPos = bodyTransform.position;
        var curVelocity = (curPos - lastPos) / (Time.time - lastVelocityUpdateTime);
        if (curVelocity.magnitude > MIN_MOVE_SPEED)
        {
            lastPos = curPos;
            bodyVelocity = curVelocity;
            lastVelocityUpdateTime = Time.time;
        }
        else {
            // only update when time is too long
            if (Time.time > lastVelocityUpdateTime + VELOCITY_MAX_SUSTAIN_INTERVAL)
            {
                lastPos = curPos;
                bodyVelocity = curVelocity;
                lastVelocityUpdateTime = Time.time;
            }
        }

        isOverMovingSpeed = bodyVelocity.magnitude > MIN_MOVE_SPEED;

        if (pickupTriggerCounter.val != previousPickupTriggerCounter) {
            previousPickupTriggerCounter = pickupTriggerCounter.val;
            grabActionTrigger = true; 
        }
    }
}
