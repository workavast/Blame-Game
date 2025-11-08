#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Avastrad.ConfigsRepositories.Editor
{
    [CustomPropertyDrawer(typeof(ConfigsRepositoryCell<>))]
    internal class EditorConfigsRepositoryCellDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var configsProp = property.FindPropertyRelative("configs");

            var height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            height += EditorGUI.GetPropertyHeight(configsProp, true) + EditorGUIUtility.standardVerticalSpacing;

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var autoFillProp = property.FindPropertyRelative("autoFill");
            var configsProp = property.FindPropertyRelative("configs");

            var handleRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(handleRect, autoFillProp);

            var configsRect = new Rect(
                position.x,
                handleRect.yMax + EditorGUIUtility.standardVerticalSpacing,
                position.width,
                EditorGUI.GetPropertyHeight(configsProp, true)
            );

            var previousGUIState = GUI.enabled;
            GUI.enabled = !autoFillProp.boolValue;
            EditorGUI.PropertyField(configsRect, configsProp, true);
            GUI.enabled = previousGUIState;
        }
    }
}
#endif
