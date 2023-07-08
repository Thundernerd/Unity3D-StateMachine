using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CustomEditor(typeof(StateMachineGraph))]
    public class StateMachineGraphInspector : Editor
    {
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            StateMachineGraph graph = (StateMachineGraph)target;

            EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Full State Machine Name", graph.FullStateMachineName);
#if HAS_ZENJECT
            EditorGUILayout.LabelField("Sub Container Manager Name", graph.SubContainerManagerName);
#endif
            EditorGUILayout.LabelField("State Id Name", graph.StateIdName);
            EditorGUILayout.LabelField("Transition Id Name", graph.TransitionIdName);
            EditorGUILayout.LabelField("Base State Name", graph.BaseStateName);
            EditorGUILayout.LabelField("Base Transition Name", graph.BaseTransitionName);
            EditorGUILayout.LabelField("State Factory Name", graph.StateFactoryName);
            EditorGUILayout.LabelField("Transition Factory Name", graph.TransitionFactoryName);
            EditorGUILayout.LabelField("Update Provider Name", graph.UpdateProviderName);

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
