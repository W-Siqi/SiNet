using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;
public class VRCPDemoRPCStub : RPCStub
{
    protected override void ProcessRemoteCall(RPCMessageBody message)
    {
        VRCPDemoRPCFunctionTable.ProcessRemoteCall(message);
    }
}
