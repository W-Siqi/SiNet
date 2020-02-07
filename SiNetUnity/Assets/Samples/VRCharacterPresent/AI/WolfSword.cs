using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class WolfSword : MonoBehaviour{
    public enum State {left,right,wander, fallOnGround, moving}

    public State state { get { return _state; } }

    [SerializeField]
    private float flySpeed = 10f;
    [SerializeField]
    private float flyAngleSpeed = 80f;
    [SerializeField]
    private float trackingAngleSpeed = 10f;
    [SerializeField]
    private float selfRotateAngleSpeed = 380f;
    [SerializeField]
    private Transform rotateChild;

    private Quaternion childInitLocalRotation;
    private WolfSwordConnection connection;
    private State _state = State.wander;
    private Rigidbody rigidbody;
    private Collider collider;
    private void Start()
    {
        childInitLocalRotation = rotateChild.localRotation;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void SetConnection(WolfSwordConnection wolfSwordConnection) {
        connection = wolfSwordConnection;
    }

    public void SlashToLeft(float slashTime) {
        if (state == State.right)
            StartCoroutine(Slashing(false, slashTime));
    }

    public void SlashToRight(float slashTime)
    {
        if (state == State.left)
            StartCoroutine(Slashing(true, slashTime));
    }

    public void BackToWolfMouth() {
        if (state == State.fallOnGround)
            FallOnGroundToWander();

        if(state == State.wander)
            StartCoroutine(BackingToMouth());
    }

    public void FlyTo(Vector3 pos) {
        StartCoroutine(FlyingTo(pos,transform.rotation));
    }

    public void FlyTo(Vector3 pos,Quaternion rotation)
    {
        StartCoroutine(FlyingTo(pos,rotation));
    }

    public void FallFromMouth() {
        if (state != State.left && state != State.right)
            return;

        _state = State.fallOnGround;
        transform.SetParent(null);
        collider.isTrigger = false;
        rigidbody.useGravity = true;
    }

    private void FallOnGroundToWander() {
        if (state == State.fallOnGround) {
            _state = State.wander;
            collider.isTrigger = true;
            rigidbody.useGravity = false;
        }
    }

    IEnumerator BackingToMouth() {
        _state = State.moving;

        var destAnchor = connection.rightStateAnchor;
        var startTime = Time.time;
        var initDistance = (transform.position - destAnchor.position).magnitude;
        var initRotate = transform.rotation;

        // to right place
        while (true) {
            // move towrads
            var moveDistance = flySpeed * Time.deltaTime;
            var moveDirection = (destAnchor.position - transform.position).normalized;
            transform.position = transform.position + moveDirection * moveDistance;

            // rotate 
            RotateTo(destAnchor.rotation,flyAngleSpeed);
            SelfClockWiseSpain(selfRotateAngleSpeed);

            // distance check
            var distance = (transform.position - destAnchor.position).magnitude;
            if (distance < flySpeed * Time.deltaTime * 2)
                break;
            yield return null;
        }
        transform.SetParent(connection.swordFixParent);
        transform.rotation = destAnchor.rotation;
        transform.position = destAnchor.position;

        // child to right angle
        while (true) {
            SelfClockWiseSpain(selfRotateAngleSpeed);
            var biasAngle = Quaternion.Angle(rotateChild.localRotation, childInitLocalRotation);
            if (biasAngle < selfRotateAngleSpeed * Time.deltaTime * 2)
                break;

            yield return null;
        }
        rotateChild.localRotation = childInitLocalRotation;

        _state = State.right;
    }

    IEnumerator Slashing(bool isToRight, float slashTime) {
        _state = State.moving;

        // to middle 
        var startTime = Time.time;
        var startLocalPos = transform.localPosition;
        var startLocalRotate = transform.localRotation;
        var toMiddleTime = slashTime / 2;
        var middleAnchor = connection.middleStateAnchor;
        while (Time.time < startTime + toMiddleTime) {
            var t = (Time.time - startTime) / toMiddleTime;
            transform.localPosition = Vector3.Lerp(startLocalPos, middleAnchor.localPosition, t);
            transform.localRotation = Quaternion.Lerp(startLocalRotate, middleAnchor.localRotation, t);
            yield return null;
        }

        // middle to dest
        startTime = Time.time;
        startLocalPos = transform.localPosition;
        startLocalRotate = transform.localRotation;
        var toDestTime = slashTime / 2;
        var targetAnchor = isToRight ? connection.rightStateAnchor : connection.leftStateAnchor;
        while (Time.time < startTime + toMiddleTime)
        {
            var t = (Time.time - startTime) / toMiddleTime;
            transform.localPosition = Vector3.Lerp(startLocalPos, targetAnchor.localPosition, t);
            transform.localRotation = Quaternion.Lerp(startLocalRotate, targetAnchor.localRotation, t);
            yield return null;
        }

        _state = isToRight ? State.right : State.left;
    }

    IEnumerator FlyingTo(Vector3 dest,Quaternion rotation) {
        _state = State.moving;

        transform.SetParent(null);
        // fly to
        while (true)
        {
            RotateTo(rotation, flyAngleSpeed);

            var targetDir = (dest - transform.position);
            ForwardDirectionCorrection(trackingAngleSpeed, targetDir);
            transform.Translate(0, 0, flySpeed * Time.deltaTime);

            SelfClockWiseSpain(selfRotateAngleSpeed);

            var distance = (dest - transform.position).magnitude;
            if (distance < flySpeed * Time.deltaTime * 2f)
                break;

            yield return null;
        }

        // to proper spain angle
        var acceptableAngle = Random.Range(10f, 35f);
        while (true)
        {
            var angleBias = Quaternion.Angle(childInitLocalRotation, rotateChild.localRotation);
            if (angleBias < acceptableAngle)
                break;

            SelfClockWiseSpain(selfRotateAngleSpeed);

            yield return null;
        }

        _state = State.wander;
    }

    void RotateTo(Quaternion dest, float angleSpeed) {
        var angleBias = Quaternion.Angle(dest, transform.rotation);
        if (angleBias > 0) {
            var maxMoveAngle = angleSpeed * Time.deltaTime;
            var t = Mathf.Min(1f, maxMoveAngle / angleBias);
            transform.rotation = Quaternion.Lerp(transform.rotation, dest, t);
        }
    }

    void ForwardDirectionCorrection(float angleSpeed, Vector3 targetDirection)
    {
        var curDirction = transform.forward;
        var biasAngle = Vector3.Angle(targetDirection, curDirction);

        if (biasAngle < 10f) {
            transform.forward = targetDirection;
        }
        else
        {
            var maxAngleChange = angleSpeed * Time.deltaTime;
            var t = maxAngleChange / biasAngle;
            t = Mathf.Min(1, t);
            var correctedDir = Vector3.Lerp(curDirction, targetDirection, t);
            transform.forward = correctedDir;
        }
    }

    void SelfClockWiseSpain(float angleSpeed) {
        rotateChild.Rotate(Vector3.up, angleSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);
    }
}
