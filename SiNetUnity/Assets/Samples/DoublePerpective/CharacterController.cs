using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movementSpeed = 10;

    private Vector3 forward {
        get {
            if (!ViewCam.instance) return Vector3.forward;

            var forward = transform.position - ViewCam.instance.realCamPos;
            forward.y = 0;
            forward.Normalize();
            return forward;
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(forward);
        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);
    }
}
