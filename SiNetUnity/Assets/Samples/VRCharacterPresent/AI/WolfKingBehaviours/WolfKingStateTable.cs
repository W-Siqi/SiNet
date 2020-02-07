using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[System.Serializable]

public class WolfKingStateTable

{
    [System.Serializable]
    public class Attribute {
        public float maxHP = 1800f;

        public float facingDistance = 1f;
        public float closeBodyDistance = 4f;

        public float runningSpeed = 3f;
        public float idleMoveSpeed = 0.3f;
        public float trunningAngleSpeed = 30f;
        public float jumpHorizonSpeed = 30f;
        public float jumpVerticalSpeed = 10f;
        public float dashSpeed = 60f;
        public float dashDistance = 8f;

        public float stunForceRate = 0.2f;
        public float stunSustainTime = 5f;

        public float swordAttackRadius = 10f;
        public float wolfBodyAttackRadius = 15f;
        public float dashAttackLatency = 0.1f;
    }

    public Attribute attribute;
    public Animator animator;
    public WolfKingSoundPlayer soundPlayer;
    public WolfKing wolfKing;
    public WolfHunter wolfFightTarget;
    public WolfSwordConnection swordConnecion;
	public Navigator navigator;
	public NavigateRoute circleRoute;
    public float currentHP = 1000f;
    [Range(0,1)]
    public float stunProcess = 0f;
    public bool isVunlerable = false;
    public bool isInDeadState = false;

    public bool isStunned { get { return stunProcess >= 1f; } }
}

