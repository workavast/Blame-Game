#if UNITY_EDITOR

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Avastrad.ConfigsRepositories.InspectorButton
{
    [CustomEditor(typeof(ConfigsRepositoryBase), true)]
    [CanEditMultipleObjects]
    internal class ButtonAttributeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var methods = target.GetType().GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );

            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<ButtonAttribute>() == null)
                    continue;

                if (GUILayout.Button(method.Name))
                    method.Invoke(target, new object[0]);
            }
        }
    }
}
#endif
