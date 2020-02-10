using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiNet;

public class VRAvatar : MonoBehaviour
{
    private enum State
    {
        idle,walk,pick
    }

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SyncTransform leftHand;
    [SerializeField]
    private SyncTransform rightHand;
    [SerializeField]
    private SyncTransform headSet;

    State state = State.idle; 

    public void OnPick() {

    }
}
