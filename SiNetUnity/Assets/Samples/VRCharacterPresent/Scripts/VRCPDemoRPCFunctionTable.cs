using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class VRCPDemoRPCFunctionTable : RPCFunctionTable
{
    private class EventName {
        // variable would be: float entitySUID float IVBottleID
        public const string GRAB_IV_BOTTLE = "grabIVBottle";
    }

    public static void BoardcastGrabIVEvent(int grabberSyncEntitySUID, int IVBottleID) {
        var variable = new RPCVariable();
        variable.stringValues.Add(EventName.GRAB_IV_BOTTLE);
        variable.floatValues.Add((float)grabberSyncEntitySUID);
        variable.floatValues.Add((float)IVBottleID);
        VRCPDemoRPCStub.instance.Call(BOARDCAST_EVENT, variable);
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
                default:
                    throw new System.Exception("undefined RPC Event");
            }

        }
        catch (System.Exception e) {
            Debug.LogError(e.Message);
        }
    }

    private static void ProcessGrabIVBottleEvent(RPCVariable rpcVariable) {
        try {
            // decode variable
            var grabberSUID = rpcVariable.floatValues[0];
            var IVBottleID = rpcVariable.floatValues[1];


        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
