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
        if (other.GetComponent<IVBottle>()) {
            var IVBottle = other.GetComponent<IVBottle>();
            IVBottle.isOnGrab = true;
            IVBottle.transform.SetParent(transform);
            IVBottle.transform.localPosition = Vector3.zero;
            VRCPDemoRPCFunctionTable.BoardcastGrabIVEvent(attachedEntity.sceneUID,IVBottle.id);
        }
    }
}
