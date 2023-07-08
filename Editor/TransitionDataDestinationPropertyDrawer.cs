using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CustomPropertyDrawer(typeof(StateMachineGraph.TransitionData.Destination))]
    public class TransitionDataDestinationPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            string parentPath = property.propertyPath.Substring(0, property.propertyPath.IndexOf(']') + 1);
            SerializedProperty parentProperty = property.serializedObject.FindProperty(parentPath);
            SerializedProperty graphProperty = parentProperty.FindPropertyRelative("graph");
            List<string> stateNames = GetStateNames(graphProperty)
                .Except(GetExistingTransitionDestinations(parentProperty))
                .Except(GetSourceState(parentProperty)).ToList();

            SerializedProperty state = property.FindPropertyRelative("state");
            GUIContent content = new(string.IsNullOrEmpty(state.stringValue) ? "-" : state.stringValue);

            if (!EditorGUI.DropdownButton(position, content, FocusType.Keyboard))
                return;
            
            GenericMenu menu = new GenericMenu();
            if (stateNames.Count == 0)
            {
                menu.AddItem(new GUIContent("-"), false, () => { });
            }
            
            foreach (string stateName in stateNames)
            {
                menu.AddItem(new GUIContent(stateName),
                    false,
                    () =>
                    {
                        state.stringValue = stateName;
                        state.serializedObject.ApplyModifiedProperties();
                    });
            }
            
            menu.ShowAsContext();
        }

        private static IEnumerable<string> GetStateNames(SerializedProperty property)
        {
            SerializedProperty statesProperty = property.serializedObject.FindProperty("states");
            string[] names = new string[statesProperty.arraySize];
            for (int i = 0; i < statesProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = statesProperty.GetArrayElementAtIndex(i);
                names[i] = elementProperty.FindPropertyRelative("name").stringValue;
            }

            return names;
        }

        private static IEnumerable<string> GetExistingTransitionDestinations(SerializedProperty property)
        {
            SerializedProperty destinationsProperty = property.FindPropertyRelative("destinations");
            string[] names = new string[destinationsProperty.arraySize];
            for (int i = 0; i < destinationsProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = destinationsProperty.GetArrayElementAtIndex(i);
                names[i] = elementProperty.FindPropertyRelative("state").stringValue;
            }

            return names;
        }

        private static IEnumerable<string> GetSourceState(SerializedProperty property)
        {
            SerializedProperty sourceProperty = property.FindPropertyRelative("source");
            return string.IsNullOrEmpty(sourceProperty.stringValue)
                ? Array.Empty<string>()
                : new[] { sourceProperty.stringValue };
        }
    }
}
