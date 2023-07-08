using System.Collections.Generic;
using System.Linq;
using TNRD.StateManagement.Contracts;
using UnityEditor;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CustomEditor(typeof(StateMachineController), true)]
    public class StateMachineControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Enter play-mode to see information", MessageType.Info, true);
                return;
            }

            IStateMachineController controller = (IStateMachineController)target;

            IState currentState = controller.StateMachine.CurrentState;
            bool isTransition = currentState is ITransition;
            EditorGUILayout.LabelField(isTransition ? "Transition:" : "Current state:",
                currentState?.StateId.ToString() ?? "-");

            if (isTransition)
                return;

            IEnumerable<PossibleTransition> possibleTransitions =
                controller.StateMachine.GetPossibleTransitions().ToList();

            EditorGUILayout.Space();

            if (!possibleTransitions.Any())
            {
                EditorGUILayout.HelpBox("No transitions available!", MessageType.Warning, true);
                return;
            }

            foreach (PossibleTransition possibleTransition in possibleTransitions)
            {
                if (GUILayout.Button("Transition to " + possibleTransition.DestinationStateId))
                {
                    controller.StateMachine.Transition(possibleTransition.TransitionId);
                }
            }
        }
    }
}
