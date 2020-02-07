using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class NavigateArea : MonoBehaviour

{

    [SerializeField]

    private BoxCollider boxCollider;

    // Start is called before the first frame update

    void Start()

    {
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;

    }



    public Vector3 centerPos { get { return transform.position;} }

    public bool IsInArea(Vector3 position) {
        return boxCollider.bounds.Contains(position);
    }

    public Vector3 GetRandomPositionInsideArea() {
        var bound = boxCollider.bounds;
        var res = bound.center;
        res += new Vector3(1, 0, 0) * Random.Range(-bound.extents.x, bound.extents.x);
        res += new Vector3(0, 0, 1) * Random.Range(-bound.extents.z, bound.extents.z);
        return res;
    }

    public Vector3 GetRandomCornorPos() {
        var bound = boxCollider.bounds;
        var res = bound.center;
        res += new Vector3(bound.extents.x, 0, 0) * (Random.value > 0.5 ? 1f : -1f);
        res += new Vector3(0, 0, bound.extents.z) * (Random.value > 0.5 ? 1f : -1f);
        return res;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(boxCollider.bounds.center, boxCollider.bounds.size);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.7f);
    }

}

