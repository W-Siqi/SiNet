using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSwordShadow : MonoBehaviour
{
    const float FLY_SPEED = 120f;
    const float COMBINE_SPEED = 60f;
    const float FLY_DISTANCE = 250f;
    const float POS_NOISE = 1f;
    public static WolfSwordShadow SummonShaowAt(Vector3 position, Vector3 forwardDirection,float flyDistance = FLY_DISTANCE) {
        position += Random.insideUnitSphere * POS_NOISE;
        var GO = Instantiate(PrefabsTable.instance.shadowSword);
        var shadowSword = GO.GetComponent<WolfSwordShadow>();
        shadowSword.FlyTo(position, forwardDirection, flyDistance);
        return shadowSword;
    }

    public void ShadowCombinedTo(Transform anchor) {
        StartCoroutine(CombinedToAnchor(anchor));
    }

    private void FlyTo(Vector3 destPos, Vector3 forwardDirection,float flyDistance) {
        StartCoroutine(FlyingTo(destPos, forwardDirection, flyDistance));
    }

    private IEnumerator FlyingTo(Vector3 destPos,Vector3 forwardDirection,float flyDistance) {
        var finishTime = flyDistance / FLY_SPEED;
        var startPos = destPos - flyDistance * forwardDirection.normalized;
        var startTime = Time.time;
        transform.forward = -forwardDirection;
        while (Time.time < startTime + finishTime) {
            var t = (Time.time - startTime) / finishTime;
            transform.position = Vector3.Lerp(startPos, destPos, t);
            yield return null;
        }
    }

    private IEnumerator CombinedToAnchor(Transform anchor) {
        var finishTime = (transform.position - anchor.position).magnitude / COMBINE_SPEED;
        var startTime = Time.time;
        var startPos = transform.position;
        var startRotate = transform.rotation;
        while (Time.time < startTime + finishTime) {
            var t = (Time.time - startTime) / finishTime;
            transform.position = Vector3.Lerp(startPos, anchor.position, t);
            transform.rotation = Quaternion.Lerp(startRotate, anchor.rotation,t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
