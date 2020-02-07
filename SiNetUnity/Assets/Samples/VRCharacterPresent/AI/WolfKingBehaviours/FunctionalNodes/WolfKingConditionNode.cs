using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public abstract class WolfKingConditionNode : DecoratorNode
    {
        protected WolfKingStateTable stateTable;
        public void Init(WolfKingStateTable stateTable)
        {
            this.stateTable = stateTable;
        }
    }
}