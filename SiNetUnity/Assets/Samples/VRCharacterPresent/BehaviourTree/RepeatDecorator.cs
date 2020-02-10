using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BehaviourTree
{
    public class RepeatDecorator : DecoratorNode
    {
        [System.Serializable]
        public class RepeatConfig
        {
            public bool isFixedCount = true;
            public int fixedCount = 2;
            public Vector2Int randomCountRange = new Vector2Int(2, 3);
        }

        [SerializeField]
        private bool isFixedCount = true;
        [SerializeField]
        private int fixedCount = 2;
        [SerializeField]
        private Vector2Int randomCountRange = new Vector2Int(2, 3);

        protected override IEnumerator NodeRunning()
        {
            _state = State.running;

            int repeatCount = isFixedCount ? fixedCount : Random.Range(randomCountRange.x, randomCountRange.y + 1);
            bool hasChildFailed = false;
            for (int i = 0; i < repeatCount; i++) {
                decorateTarget.ExecuteNode();
                while (true) {
                    if (decorateTarget.state == State.success)
                    {
                        break;
                    }
                    else if (decorateTarget.state == State.fail) {
                        hasChildFailed = true;
                        break;
                    }

                    yield return null;
                }

                if (hasChildFailed)
                    break;
            }

            if (hasChildFailed)
                _state = State.fail;
            else
                _state = State.success;
        }
    }
}