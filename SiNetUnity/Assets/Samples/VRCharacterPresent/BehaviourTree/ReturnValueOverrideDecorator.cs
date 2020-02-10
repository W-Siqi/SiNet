using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
    public class ReturnValueOverrideDecorator :DecoratorNode
    {
        [SerializeField]
        bool alwaysSuccess = false;
        [SerializeField]
        bool alwaysFail = false;

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            decorateTarget.ExecuteNode();
            while (true) {
                if (decorateTarget.state != State.running)
                        break;
                yield return null;
            }

            if (alwaysFail)
                _state = State.fail;
            else if (alwaysSuccess)
                _state = State.success;
            else
                _state = decorateTarget.state;
        }
    }
}
