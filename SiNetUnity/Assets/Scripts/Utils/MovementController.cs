using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class MovementController : MonoBehaviour
{
    public SyncTransform attachedTransform;

    private void Update()
    {
        if (attachedTransform) {
            transform.position = attachedTransform.position;
            transform.localScale = attachedTransform.localScale;
            transform.rotation = attachedTransform.rotation;
        }
    }
}
