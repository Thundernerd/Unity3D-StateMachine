using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CustomEditor(typeof(StateMachineGraph))]
    public class StateMachineGraphInspector : OdinEditor
    {
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginDisabledGroup(!IsValid());
            if (GUILayout.Button("Generate"))
            {
                StateMachineGraphGenerator.Generate((StateMachineGraph)target);
            }
            EditorGUI.EndDisabledGroup();
        }
        
        private bool IsValid()
        {
            StateMachineGraph graph = (StateMachineGraph)target;

            if (string.IsNullOrEmpty(graph.StateMachineName))
                return false;
            if (string.IsNullOrEmpty(graph.Namespace))
                return false;
            if (string.IsNullOrEmpty(graph.Destination))
                return false;
            return graph.States.Count(x => x.IsInitialState) == 1;
        }
    }
}
