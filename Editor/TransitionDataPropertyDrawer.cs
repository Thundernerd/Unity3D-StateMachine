using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CustomPropertyDrawer(typeof(StateMachineGraph.TransitionData))]
    public class TransitionDataPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) +
                   EditorGUIUtility.standardVerticalSpacing +
                   EditorGUI.GetPropertyHeight(property.FindPropertyRelative("destinations"));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawSourceProperty(position, property);

            SerializedProperty destinationsProperty = property.FindPropertyRelative("destinations");
            float height = EditorGUI.GetPropertyHeight(destinationsProperty);
            EditorGUI.PropertyField(new Rect(position.x,
                    position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                    position.width,
                    height),
                destinationsProperty,
                true);
        }

        private void DrawSourceProperty(Rect position, SerializedProperty property)
        {
            SerializedProperty source = property.FindPropertyRelative("source");

            Rect rect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            GUIContent content = new(string.IsNullOrEmpty(source.stringValue) ? "-" : source.stringValue);
            if (!EditorGUI.DropdownButton(rect, content, FocusType.Keyboard))
                return;

            GenericMenu menu = new GenericMenu();
            List<string> availableStates = GetStateNames(property)
                .Except(GetExistingTransitionSources(property))
                .ToList();

            if (availableStates.Count == 0)
            {
                menu.AddItem(new GUIContent("-"), false, () => { });
            }

            foreach (string availableState in availableStates)
            {
                menu.AddItem(new GUIContent(availableState),
                    false,
                    () =>
                    {
                        source.stringValue = availableState;
                        source.serializedObject.ApplyModifiedProperties();
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

        private static IEnumerable<string> GetExistingTransitionSources(SerializedProperty property)
        {
            SerializedProperty transitionsProperty = property.serializedObject.FindProperty("transitions");
            string[] names = new string[transitionsProperty.arraySize];
            for (int i = 0; i < transitionsProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = transitionsProperty.GetArrayElementAtIndex(i);
                names[i] = elementProperty.FindPropertyRelative("source").stringValue;
            }

            return names;
        }
    }
}
