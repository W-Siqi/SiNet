using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCam : MonoBehaviour
{
    public Transform focusPoint;
    public float sensitivityYaw = 30f;
    public float sensitivityPitch = 30f;
    public Camera cam;
    public bool rotateLocked = false;
    static ViewCam _instance = null;

    public static ViewCam instance {
        get {
            if (!_instance)
                _instance = FindObjectOfType<ViewCam>();

            return _instance;
        }
    }

    public Vector3 realCamPos {
        get {
            if (cam){
                return cam.transform.position;
            }
            else {
                return Vector3.zero;
            }
        }
    }

    private void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotateLocked) {
            var x1 = Input.GetAxis("Mouse X");
            transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * sensitivityYaw * x1, Vector3.up);
        }

        if (focusPoint) {
            transform.position = focusPoint.position;
        }
    }

    public void OpenCam() {
        cam.enabled = true;
    }
}
