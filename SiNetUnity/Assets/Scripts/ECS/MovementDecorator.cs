using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    // sometimes we need different treatment & configure of movement system
    // then attach decorator to the SyncEntity
    public class MovementDecorator : MonoBehaviour
    {
        public bool applyMovementSystem = true;
        public bool ignoreYPosition = false;
        public bool lockAtYRotation = false;
    }
}
