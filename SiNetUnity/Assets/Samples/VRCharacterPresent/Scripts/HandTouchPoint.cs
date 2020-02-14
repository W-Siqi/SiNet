using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

[RequireComponent(typeof(Collider))]
public class HandTouchPoint : MonoBehaviour
{
    public SyncEntity attachedEntity;
    public SyncInt openWindowTriggerCounter;
    private void OnTriggerEnter(Collider other)
    {
        var bottle = other.GetComponent<IVBottle>();
        if (bottle) {
            OnTouchIVBottle(bottle);
            return;
        }

        var window = other.GetComponent<WindowOfWeather>();
        if (window) {
            OnTouchWindowOfWeather(window);
            return;
        }
    }

    private void OnTouchIVBottle(IVBottle bottle) {
        if (!bottle.isOnGrab) {
            bottle.isOnGrab = true;
            bottle.transform.SetParent(transform);
            bottle.transform.localPosition = Vector3.zero;
            VRCPDemoRPCFunctionTable.BoardcastGrabIVEvent(attachedEntity.sceneUID, bottle.id);
        }
    }

    private void OnTouchWindowOfWeather(WindowOfWeather window) {
        if (!window.isOpen) {
            VRCPDemoRPCFunctionTable.BoardcastOpenWindowEvent();
            openWindowTriggerCounter.val++;
        }
    }
}
