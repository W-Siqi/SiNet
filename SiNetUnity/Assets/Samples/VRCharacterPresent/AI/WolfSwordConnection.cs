using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSwordConnection : MonoBehaviour
{
    public WolfSword attachedSword { get { return connectedSword; } }

    public Transform swordFixParent;
    public Transform leftStateAnchor;
    public Transform rightStateAnchor;
    public Transform middleStateAnchor;
    public Transform ultimateSkillAnchor;
    public WolfSword connectedSword;

    // Start is called before the first frame update
    void Start()
    {
        if (!connectedSword)
            connectedSword = FindObjectOfType<WolfSword>();
        connectedSword.SetConnection(this);
    } 
}
