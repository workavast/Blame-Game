using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace App.Utils.Polymorphism.Editor
{
    [CustomPropertyDrawer(typeof(PolymorphicAttribute))]
    public class PolymorphicReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Handle array/list elements
            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                DrawManagedReference(position, property, label, fieldInfo.FieldType);
            }
            else
            {
                // Fallback for non-managed reference properties
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
                return EditorGUI.GetPropertyHeight(property, label, true);

            var bodyHeight = EditorGUI.GetPropertyHeight(property, GUIContent.none, true);
            return EditorGUIUtility.singleLineHeight + 4 + bodyHeight;
        }

        private static void DrawManagedReference(Rect position, SerializedProperty property, GUIContent label, System.Type fieldType)
        {
            EditorGUI.BeginProperty(position, label, property);

            var baseType = GetBaseTypeFromFieldType(fieldType);
            var types = TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t => !t.IsAbstract && !t.IsGenericType)
                .OrderBy(t => t.Name)
                .ToList();

            var options = new List<string> { "None" };
            options.AddRange(types.Select(t => t.Name));

            // Determine current selection
            int currentIndex = 0; // None
            if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                // managedReferenceFullTypename example: "Assembly-CSharp Some.Namespace.TypeName"
                var parts = property.managedReferenceFullTypename.Split(' ');
                var fullname = parts.Length >= 2 ? parts[1] : parts[0];
                for (int i = 0; i < types.Count; i++)
                {
                    if (types[i].FullName == fullname)
                    {
                        currentIndex = i + 1; // +1 because of None
                        break;
                    }
                }
            }

            var rectType = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var newIndex = EditorGUI.Popup(rectType, label.text, currentIndex, options.ToArray());

            if (newIndex != currentIndex)
            {
                if (newIndex == 0)
                {
                    property.managedReferenceValue = null;
                }
                else
                {
                    var t = types[newIndex - 1];
                    var instance = Activator.CreateInstance(t);
                    property.managedReferenceValue = instance;
                }
                property.serializedObject.ApplyModifiedProperties();
            }

            var rectBody = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 4, position.width, position.height - EditorGUIUtility.singleLineHeight - 4);
            EditorGUI.PropertyField(rectBody, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }
        
        private static Type GetBaseTypeFromFieldType(Type fieldType)
        {
            // Handle arrays
            if (fieldType.IsArray)
                return fieldType.GetElementType();

            // Handle List<T>
            if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
                return fieldType.GetGenericArguments()[0];

            // Return as-is for single fields
            return fieldType;
        }
    }
}