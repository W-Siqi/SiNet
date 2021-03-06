﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class VRCPDemoRPCFunctionTable : RPCFunctionTable
{
    private class EventName {
        // variable would be: float entitySUID float IVBottleID
        public const string GRAB_IV_BOTTLE = "grabIVBottle";
        public const string OPEN_WINDOW = "openWindow";
    }
    public static void ProcessRemoteCall(RPCMessageBody message) {
        switch (message.rpcSignature.name) {
            case BOARDCAST_EVENT:
                ProcessRemoteEventCall(message.rpcVariable);
                break;
            default:
                throw new System.Exception("undefined RPC Call");
        }
    }

    private static void ProcessRemoteEventCall(RPCVariable rpcVariable) {
        try
        {
            // decode the varibale
            var eventName = rpcVariable.stringValues[0];

            switch (eventName)
            {
                case EventName.GRAB_IV_BOTTLE:
                    ProcessGrabIVBottleEvent(rpcVariable);
                    break;
                case EventName.OPEN_WINDOW:
                    ProcessOpenWindowEvent();
                    break;
                default:
                    throw new System.Exception("undefined RPC Event");
            }

        }
        catch (System.Exception e) {
            Debug.LogError(e.Message);
        }
    }

    public static void BoardcastGrabIVEvent(int grabberSyncEntitySUID, int IVBottleID)
    {
        var variable = new RPCVariable();
        variable.stringValues.Add(EventName.GRAB_IV_BOTTLE);
        variable.floatValues.Add((float)grabberSyncEntitySUID);
        variable.floatValues.Add((float)IVBottleID);
        VRCPDemoRPCStub.instance.Call(BOARDCAST_EVENT, variable);
    }

    public static void BoardcastOpenWindowEvent()
    {
        var variable = new RPCVariable();
        variable.stringValues.Add(EventName.OPEN_WINDOW);
        VRCPDemoRPCStub.instance.Call(BOARDCAST_EVENT, variable);
    }


    private static void ProcessGrabIVBottleEvent(RPCVariable rpcVariable) {
        try {
            // decode variable
            var grabberSUID = (int)rpcVariable.floatValues[0];
            var IVBottleID = (int)rpcVariable.floatValues[1];

            var targetBlackBoard = SyncVRNurseBlackboard.FindBlackBoard(grabberSUID);
            var targetIVBottle = IVBottle.FindIVBottle(IVBottleID);

            // set the blackboard values
            targetBlackBoard.grabIVBottleID = targetIVBottle.id;
            targetBlackBoard.grabActionTrigger = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private static void ProcessOpenWindowEvent(){
        PerspectiveSystem.instance.ExecutePerspectiveEvent(
            PerspectiveSystem.PerspectiveEvent.openWindow);
    }
}
