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
    SyncEntity attachedEntity;
    [SerializeField]
    SyncInt openWindowTriggerCounter;

    public Animator animator;
    public MovementDecorator movementDecorator;
    public Transform handAnchorPoint;

    public bool isOverMovingSpeed = false;
    public bool grabActionTrigger = false;
    public bool openWindowTrigger = false;

    public Vector3 bodyVelocity = Vector3.zero;
    public int grabIVBottleID = -1;


    public bool hasAction {
        get {
            return grabActionTrigger || openWindowTrigger;
        }
    }

    private int previousOpenWindowTriggerCounter = 0;
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

        // update moving status
        isOverMovingSpeed = bodyVelocity.magnitude > MIN_MOVE_SPEED;

        // open window trigger
        if (openWindowTriggerCounter.val != previousOpenWindowTriggerCounter) {
            previousOpenWindowTriggerCounter = openWindowTriggerCounter.val;
            openWindowTrigger = true; 
        }
    }

    public static SyncVRNurseBlackboard FindBlackBoard(int attachedEntityID)
    {
        foreach(var bb in FindObjectsOfType<SyncVRNurseBlackboard>()) {
            if (bb.attachedEntity && bb.attachedEntity.sceneUID == attachedEntityID)
                return bb;
        }
        return null;
    }
}
