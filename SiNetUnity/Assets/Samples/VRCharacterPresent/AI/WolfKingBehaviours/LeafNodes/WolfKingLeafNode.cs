using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    public abstract class  WolfKingLeafNode : LeafNode{
        protected WolfKingStateTable stateTable = null;

        public void Init(WolfKingStateTable stateTable) {
            this.stateTable = stateTable;
        }
    }}