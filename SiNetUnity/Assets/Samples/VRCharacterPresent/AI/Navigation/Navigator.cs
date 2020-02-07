using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour{
    public class NavigationTrackingInfo {
        public bool isFinished;
    }

    [SerializeField]
    private NavigateArea navigateArea;
    [SerializeField]
    private float heightCorrectBias = 0f;
    [SerializeField]
    private bool heightCorrectionOn = false;

    private int currentNavID = 0;

    private void Update()
    {
        if(heightCorrectionOn)
            HeightCorretion();
    }

    public NavigationTrackingInfo RunRouteFromStartToEnd(NavigateRoute route,float speed,float angleSpeed) {
        // Debug.Log("NAVIGATION: run route");
        var trackingInfo = new NavigationTrackingInfo();
        StartCoroutine(ExeRunRouteFromStartToEnd(++currentNavID, route, speed, angleSpeed, trackingInfo));
        return trackingInfo;
    }

    public NavigationTrackingInfo Chase(Transform target, float speed,float angleSpeed, float reachDistance)
    {
        // Debug.Log("NAVIGATION: chase");
        var trackingInfo = new NavigationTrackingInfo();
        trackingInfo.isFinished = false;
        StartCoroutine(ExeChase(++currentNavID, target, speed, angleSpeed,reachDistance, trackingInfo));
        return trackingInfo;
    }

    public NavigationTrackingInfo GoToPosition(Vector3 position, float speed, float angleSpeed)
    {
        // Debug.Log("NAVIGATION: goto pos");
        var trackingInfo = new NavigationTrackingInfo();
        trackingInfo.isFinished = false;
        StartCoroutine(ExeGoToPosition(++currentNavID, position, speed, angleSpeed, trackingInfo));
        return trackingInfo;
    }

    public NavigationTrackingInfo GoToAreaCenter(float speed, float angleSpeed)
    {
        return GoToPosition(navigateArea.centerPos, speed, angleSpeed);
    }

    public NavigationTrackingInfo GoToRandomPosition(float speed, float angleSpeed)
    {
        var randomPos = navigateArea.GetRandomPositionInsideArea();
        return GoToPosition(randomPos, speed, angleSpeed);
    }

    public NavigationTrackingInfo Jump(float forwardSpeed, float upSpeed)
    {
        var trackingInfo = new NavigationTrackingInfo();
        StartCoroutine(ExeJump(++currentNavID, forwardSpeed, upSpeed, trackingInfo));
        return trackingInfo;
    }

    public NavigationTrackingInfo Dash(float dashSpeed, float dashDistance)
    {
        var trackingInfo = new NavigationTrackingInfo();
        StartCoroutine(ExeDash(++currentNavID,dashSpeed,dashDistance,trackingInfo));
        return trackingInfo;
    }

    public NavigationTrackingInfo ToCornor(float speed,float angleSpeed) {
        return GoToPosition(navigateArea.GetRandomCornorPos(), speed, angleSpeed);
    }

    //   public NavigationTrackingInfo RunRouteForOneCircle(NavigateRoute route,float speed) {
    //	return null;
    //}

    public void RandomRun(Vector2 switchTimeRange,float speed, float angleSpeed) {
        StartCoroutine(ExeRandomRun(++currentNavID, switchTimeRange, speed, angleSpeed));
    }

    public void BackAndForth(float moveSpeed) {
        // Debug.Log("NAVIGATION: backAngForth");
        StartCoroutine(ExeBackAndForth(++currentNavID, moveSpeed));
    }

    public void Facing(Transform target, float speed, float turnningAngleSpeed, float facingDistance) {
        // .Log("NAVIGATION: facing");
        StartCoroutine(ExeFacing(++currentNavID, target, speed, turnningAngleSpeed, facingDistance));
    }

    public void StopNavigating() {
        // Debug.Log("NAVIGATION: stop Navi received");
        currentNavID++;
    }

    IEnumerator ExeChase(int navID, Transform target, float speed, float angleSpeed, float reachDistance, NavigationTrackingInfo trackingInfo)
    {
        trackingInfo.isFinished = false;
        while (navID == currentNavID)
        {
            // correct direction
            var targetDir = target.position - transform.position;
            DirectionCorrection(angleSpeed, targetDir);
            ForwardYCorrection();

            // move
            transform.Translate(0, 0, speed * Time.deltaTime);

            // distance Check
            var distance = CalFlatDistance(target.position, transform.position);
            if (distance < reachDistance)
                break;

            yield return null;
        }

        trackingInfo.isFinished = true;
    }

    IEnumerator ExeGoToPosition(int navID, Vector3 position, float speed, float angleSpeed, NavigationTrackingInfo trackingInfo) {
        trackingInfo.isFinished = false;

        while (navID == currentNavID) {    
            // correct direction
            var targetDir = position - transform.position;
            DirectionCorrection(angleSpeed, targetDir);
            ForwardYCorrection();

            // move
            transform.Translate(0, 0, speed * Time.deltaTime);

            // distance Check
            var distance = CalFlatDistance(position, transform.position);

            if (distance < speed * Time.deltaTime * 2f)
                break;

            yield return null;
        }

        trackingInfo.isFinished = true;
    }

    IEnumerator ExeRunRouteFromStartToEnd(int navID, NavigateRoute navigateRoute, float speed, float angleSpeed, NavigationTrackingInfo trackingInfo) {
        trackingInfo.isFinished = false;

        for (int i = 0; i < navigateRoute.routeNodes.Length && navID == currentNavID; i++) {
            var node = navigateRoute.routeNodes[i];

            // remenber to use name nav ID
            var nodeTrackingInfo = new NavigationTrackingInfo();
            StartCoroutine(ExeGoToPosition(navID, node.position, speed, angleSpeed, nodeTrackingInfo));

            // wait finish or interupted 
            while (true) {
                if (nodeTrackingInfo.isFinished || navID != currentNavID)
                    break;

                yield return null;
            }
        }

        trackingInfo.isFinished = true;
    }

    IEnumerator ExeBackAndForth(int navID, float moveSpeed){
        float MIN_TIME = 1f;
        float MAX_TIME = 3f;
        float MAX_ANGLE_SPEED = 50f;

        bool isForth = Random.Range(0, 1f) > 0.5f ? true : false;
        while (currentNavID == navID) {
            var sustainTime = Random.Range(MIN_TIME, MAX_TIME);
            var endTime = Time.time + sustainTime;
            var angleSpeed = Random.Range(0f, MAX_ANGLE_SPEED);
            var forthFactor = isForth ? 1f : -1f;
            var rightFactor = Random.Range(0, 1f) > 0.5f ? 1f : -1f;

            // move once
            while (Time.time < endTime && currentNavID == navID) {
                var moveDistance = moveSpeed * Time.deltaTime;
                var moveAngle = angleSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up, rightFactor * moveAngle);
                transform.Translate(0, 0, forthFactor * moveDistance);
                yield return null;
            }

            isForth = !isForth;
        }      
    }

    IEnumerator ExeFacing(int navID, Transform target, float speed, float turnningAngleSpeed, float facingDistance) {
        var DIRECTION_UPDATE_INTERVAL = 0.5f;
        var directionFactor = -1f;
        var nextDirectionUpdateTime = 0f;
        while (currentNavID == navID) {
            // angle correct
            var targetDir = target.position - transform.position;
            DirectionCorrection(turnningAngleSpeed,targetDir);
            ForwardYCorrection();
            
            // distance correct
            if (Time.time > nextDirectionUpdateTime) {
                var distance = CalFlatDistance(transform.position, target.position);

                if (distance < facingDistance)
                    directionFactor = -1f;     
                else 
                    directionFactor = Random.value > 0.5f ? 1f : -1f;

                nextDirectionUpdateTime = Time.time + DIRECTION_UPDATE_INTERVAL;
            }
           
            // move
            transform.Translate(0, 0, directionFactor * speed * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator ExeRandomRun(int navID, Vector2 switchTimeRange, float speed, float angleSpeed) {
        throw new System.NotImplementedException();
    }

    IEnumerator ExeJump(int navID, float forwardSpeed, float upSpeed, NavigationTrackingInfo trackingInfo) {
        trackingInfo.isFinished = false;

        ForwardYCorrection();

        var initHeight = transform.position.y;
        var GRAVITY = 9.8f;
        var verticalSpeed = upSpeed;
        while (true) {
            verticalSpeed -= GRAVITY * Time.deltaTime;
            transform.Translate(0, verticalSpeed*Time.deltaTime, forwardSpeed * Time.deltaTime);

            if (navID != currentNavID) {
                forwardSpeed = 0;
            }

            if (transform.position.y < initHeight) {
                var prePos = transform.position;
                prePos.y = initHeight;
                transform.position = prePos;
                break;
            }
            
            yield return null;
        }

        trackingInfo.isFinished = true;
    }

    IEnumerator ExeDash(int navID, float dashSpeed, float dashDistance,NavigationTrackingInfo trackingInfo) {
        trackingInfo.isFinished = false;

        var startPos = transform.position;
        var startRotate = transform.rotation;
        var dashDestPos = transform.position + transform.forward * dashDistance;

        float DASH_ANGLE = 40f;
        transform.Rotate(Vector3.up, DASH_ANGLE);
        var dashDestRotate = transform.rotation;
        transform.Rotate(Vector3.up, -DASH_ANGLE);
        var dashTime = dashDistance / dashSpeed;
        var startTime = Time.time;

        while (Time.time < startTime + dashTime){
            if (navID != currentNavID)
                break;

            var t = (Time.time - startTime) / dashTime;
            transform.position = Vector3.Lerp(startPos, dashDestPos, t);
            transform.rotation = Quaternion.Lerp(startRotate, dashDestRotate, t);

            yield return null;
        }

        trackingInfo.isFinished = true;
    }

    void DirectionCorrection(float angleSpeed,Vector3 targetDirection) {
        var curDirction = transform.forward;
        var biasAngle = Vector3.Angle(targetDirection, curDirction);
        if (biasAngle != 0)
        {
            var maxAngleChange = angleSpeed * Time.deltaTime;
            var t = maxAngleChange / biasAngle;
            t = Mathf.Min(1, t);
            var correctedDir = Vector3.Lerp(curDirction, targetDirection, t);
            transform.forward = correctedDir;
        }
    }

    void ForwardYCorrection() {
        var correctedForward = transform.forward;
        correctedForward.y = 0;
        transform.forward = correctedForward;
    }

    float CalFlatDistance(Vector3 point1,Vector3 point2) {
        point1.y = 0;
        point2.y = 0;
        return (point1 - point2).magnitude;
    }

    void HeightCorretion() {
        var newPos = GetGroundPos(transform.position);
        newPos += Vector3.up * heightCorrectBias;
        transform.position = newPos;
    }

    private Vector3 GetGroundPos(Vector3 currentPos)
    {
        var hit = new RaycastHit();
        var detectPos = currentPos + Vector3.up * 3f;
        if (Physics.Raycast(detectPos, Vector3.down, out hit, 100f, (1 << LayerMask.NameToLayer("StaticScene")), QueryTriggerInteraction.Ignore))
        {
            return hit.point;
        }
        else
        {
            return currentPos;
        }
    }
}

