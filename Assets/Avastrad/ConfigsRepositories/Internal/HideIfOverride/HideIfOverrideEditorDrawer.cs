#if UNITY_EDITOR

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Avastrad.ConfigsRepositories.HideIfOverride
{
    [CustomPropertyDrawer(typeof(HideIfOverrideAttribute))]
    internal class HideIfOverrideEditorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject;
            var attr = (HideIfOverrideAttribute)attribute;

            var objectType = targetObject.GetType();
            var method = objectType.GetMethod(attr.MethodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method == null)
            {
                EditorGUI.LabelField(position, $"Method '{attr.MethodName}' not found");
                return;
            }

            bool isOverridden = method.DeclaringType != method.GetBaseDefinition().DeclaringType;

            if (!isOverridden)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject;
            var attr = (HideIfOverrideAttribute)attribute;

            var objectType = targetObject.GetType();
            var method = objectType.GetMethod(attr.MethodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method == null || method.DeclaringType == method.GetBaseDefinition().DeclaringType)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            return 0;
        }
    }
}
#endif
