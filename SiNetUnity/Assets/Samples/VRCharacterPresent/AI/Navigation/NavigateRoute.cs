using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavigateRoute : MonoBehaviour
{
    public Transform[] routeNodes;
    // Start is called before the first frame update
    void Start()
    {
        var nodes = new List<Transform>();
        foreach (var node in GetComponentsInChildren<Transform>()) {
            if (node != transform)
                nodes.Add(node);
        }
        routeNodes = nodes.ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var node in routeNodes) {
            Gizmos.DrawSphere(node.position, 0.3f);
        }

        for (int i = 1; i < routeNodes.Length; i++) {
            Gizmos.DrawLine(routeNodes[i - 1].position, routeNodes[i].position);
        }
    }
}
