using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

[RequireComponent(typeof(Collider))]
public class HandTouchPoint : MonoBehaviour
{
    public SyncEntity attachedEntity;

    private void OnTriggerEnter(Collider other)
    {
        var IVBottle = other.GetComponent<IVBottle>();
        if (IVBottle && !IVBottle.isOnGrab) {
            IVBottle.isOnGrab = true;
            IVBottle.transform.SetParent(transform);
            IVBottle.transform.localPosition = Vector3.zero;
            VRCPDemoRPCFunctionTable.BoardcastGrabIVEvent(attachedEntity.sceneUID,IVBottle.id);
        }
    }
}
