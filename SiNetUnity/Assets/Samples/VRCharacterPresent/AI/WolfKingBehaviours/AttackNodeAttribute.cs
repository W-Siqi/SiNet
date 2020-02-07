using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    [System.Serializable]
    public class WolfKingAttackNodeAttribute
    {
        public float attackValue = 100f;
        public bool defendable = true;
        public float force = 1f;
    }
}