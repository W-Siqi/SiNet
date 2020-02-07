using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviourTree {
#if UNITY_EDITOR
    [CustomEditor(typeof(DebugRoot))]
    public class DebugRootEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var debugRoot = (DebugRoot)target;
            if (GUILayout.Button("Run Node")) {
                debugRoot.debugTarget.ExecuteNode();
            }
        }
    }
#endif
}
