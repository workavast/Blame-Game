#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace App.TypeReferencing
{
    [CustomPropertyDrawer(typeof(TypeReference<>))]
    internal class TypeReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeNameProperty = property.FindPropertyRelative("typeName");
            var dropdownOptions = new List<string> { "None" };
            var types = new List<Type> { null };
        
            // Take T generic type of TypeReference<T>
            var baseType = fieldInfo.FieldType.GetGenericArguments()[0];

            var derivedTypes = GetDerivedTypes(baseType);
            foreach (var type in derivedTypes)
            {
                dropdownOptions.Add(type.Name);
                types.Add(type);
            }
        
            // Current selected type
            var currentTypeName = typeNameProperty.stringValue;
            var currentType = !string.IsNullOrEmpty(currentTypeName) ? Type.GetType(currentTypeName) : null;
            var currentIndex = types.IndexOf(currentType);
            if (currentIndex == -1) 
                currentIndex = 0;
        
            // Selection dropdown
            var selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, dropdownOptions.ToArray());
            if (selectedIndex != currentIndex) 
                typeNameProperty.stringValue = types[selectedIndex]?.AssemblyQualifiedName ?? "";
        }
    
        private static List<Type> GetDerivedTypes(Type baseType)
        {
            var derivedClasses = new List<Type>();
            if (!baseType.IsAbstract) 
                derivedClasses.Add(baseType);
            
            derivedClasses.AddRange(TypeCache.GetTypesDerivedFrom(baseType));
            for (var i = 0; i < derivedClasses.Count; i++)
                if (derivedClasses[i].IsAbstract)
                    derivedClasses.RemoveAt(i);

            return derivedClasses;
        }
    }
}
#endif