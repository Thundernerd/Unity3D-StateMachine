using UnityEditor;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CustomPropertyDrawer(typeof(StateMachineGraph.StateData))]
    public class StateDataPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ShouldDrawIsInitialStateProperty(property))
            {
                return base.GetPropertyHeight(property, label) * 2 + EditorGUIUtility.standardVerticalSpacing;
            }
            else
            {
                return base.GetPropertyHeight(property, label);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("name"));

            if (ShouldDrawIsInitialStateProperty(property))
            {
                SerializedProperty isInitialStateProperty = property.FindPropertyRelative("isInitialState");
                EditorGUI.PropertyField(new Rect(position.x,
                        position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                        position.width,
                        EditorGUIUtility.singleLineHeight),
                    isInitialStateProperty);
            }
        }

        private bool ShouldDrawIsInitialStateProperty(SerializedProperty property)
        {
            SerializedProperty graphProperty = property.FindPropertyRelative("graph");
            SerializedProperty graphStatesProperty = graphProperty.serializedObject.FindProperty("states");

            bool otherIsInitialState = false;
            for (int i = 0; i < graphStatesProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = graphStatesProperty.GetArrayElementAtIndex(i);
                otherIsInitialState |= elementProperty.FindPropertyRelative("isInitialState").boolValue;
            }

            SerializedProperty isInitialStateProperty = property.FindPropertyRelative("isInitialState");
            return isInitialStateProperty.boolValue || !otherIsInitialState;
        }
    }
}
