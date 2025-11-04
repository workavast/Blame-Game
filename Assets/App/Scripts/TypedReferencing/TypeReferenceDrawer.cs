using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace App.TypedReferencing
{
    [CustomPropertyDrawer(typeof(TypeReference<>))]
    public class TypeReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeNameProperty = property.FindPropertyRelative("typeName");
            var cashedTypesNames = property.FindPropertyRelative("cashedDerivedTypes");
            var updateTimerProperty = property.FindPropertyRelative("updateTimer");

            var options = new List<string> { "None" };
            var types = new List<Type> { null };
            List<string> cashedAssemblyQualifiedNames = null;

            var updateTimerValue = updateTimerProperty.floatValue;
            updateTimerValue += Time.deltaTime;
            var shouldBeUpdated = updateTimerValue >= 2;
        
            if (shouldBeUpdated || cashedTypesNames.arraySize <= 0)
            {
                updateTimerProperty.floatValue = 0;
            
                // Получаем generic параметр
                var baseType = fieldInfo.FieldType.GetGenericArguments()[0];
                var derivedTypes = GetDerivedTypes(baseType);
            
                cashedAssemblyQualifiedNames = new List<string>(derivedTypes.Count);
        
                foreach (var type in derivedTypes)
                {
                    cashedAssemblyQualifiedNames.Add(type.AssemblyQualifiedName);
                    options.Add(type.Name);
                    types.Add(type);
                }

                // Кешируем AssemblyQualifiedNames
                cashedTypesNames.ClearArray();
                for (var i = 0; i < cashedAssemblyQualifiedNames.Count; i++)
                {
                    cashedTypesNames.InsertArrayElementAtIndex(i);
                    var element = cashedTypesNames.GetArrayElementAtIndex(i);
                    element.stringValue = cashedAssemblyQualifiedNames[i];
                }
            }
            else
            {
                updateTimerProperty.floatValue = updateTimerValue;
            
                cashedAssemblyQualifiedNames = new List<string>(cashedTypesNames.arraySize);
                for (var i = 0; i < cashedTypesNames.arraySize; i++) 
                    cashedAssemblyQualifiedNames.Add(cashedTypesNames.GetArrayElementAtIndex(i).stringValue);
        
                foreach (var assemblyQualifiedName in cashedAssemblyQualifiedNames)
                {
                    var type = Type.GetType(assemblyQualifiedName);
                    if (type != null)
                    {
                        options.Add(type.Name);
                        types.Add(type);
                    }
                }
            }
        
            // Текущий выбранный тип
            var currentTypeName = typeNameProperty.stringValue;
            var currentType = !string.IsNullOrEmpty(currentTypeName) ? Type.GetType(currentTypeName) : null;
            var currentIndex = types.IndexOf(currentType);
            if (currentIndex == -1) 
                currentIndex = 0;
        
            // Выпадающий список
            var selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, options.ToArray());
            if (selectedIndex != currentIndex) 
                typeNameProperty.stringValue = types[selectedIndex]?.AssemblyQualifiedName ?? "";
        }
    
        private List<Type> GetDerivedTypes(Type baseType)
        {
            var types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
            foreach (var assembly in assemblies)
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                        if (type.IsClass && !type.IsAbstract && baseType.IsAssignableFrom(type)) 
                            types.Add(type);
                }
                catch (System.Reflection.ReflectionTypeLoadException) { }
            }
        
            return types;
        }
    }
}
#endif