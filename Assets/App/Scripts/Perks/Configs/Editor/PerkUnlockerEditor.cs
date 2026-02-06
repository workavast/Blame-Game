using System.Collections.Generic;
using App.Perks.Configs;
using UnityEditor;
using UnityEngine;

namespace App.Perks.Configs.Editor
{
    public class PerkUnlockerEditor : EditorWindow
    {
        [SerializeField] private List<PerkConfig> configs = new();
        [SerializeField] private bool unlockedValue = true;

        [MenuItem("Tools/Perks/Set unlockedByDefault...")]
        public static void ShowWindow() 
            => GetWindow<PerkUnlockerEditor>("Perk Unlocker");

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Set unlockedByDefault on PerkConfig assets", EditorStyles.boldLabel);
            unlockedValue = EditorGUILayout.Toggle("Unlocked value", unlockedValue);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Drag & drop PerkConfig assets into the list below:");

            var so = new SerializedObject(this);
            var prop = so.FindProperty("configs");
            EditorGUILayout.PropertyField(prop, true);
            so.ApplyModifiedProperties();

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Apply to list"))
                ApplyToList();
            
            if (GUILayout.Button("Apply to selected in Project")) 
                ApplyToSelection();
        }

        private void ApplyToList()
        {
            if (configs == null || configs.Count == 0)
            {
                EditorUtility.DisplayDialog("No configs", "No PerkConfig assets in the list.", "OK");
                return;
            }

            var changed = 0;
            foreach (var cfg in configs)
            {
                if (cfg == null) 
                    continue;
                if (SetUnlocked(cfg, unlockedValue)) 
                    changed++;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("Done", $"Updated {changed} assets.", "OK");
        }

        private void ApplyToSelection()
        {
            var objs = Selection.objects;
            var changed = 0;
            foreach (var o in objs)
            {
                var cfg = o as PerkConfig;
                if (cfg == null)
                {
                    var path = AssetDatabase.GetAssetPath(o);
                    if (string.IsNullOrEmpty(path)) continue;
                    cfg = AssetDatabase.LoadAssetAtPath<PerkConfig>(path);
                }

                if (cfg == null) 
                    continue;
                
                if (SetUnlocked(cfg, unlockedValue)) 
                    changed++;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("Done", $"Updated {changed} assets.", "OK");
        }

        private bool SetUnlocked(PerkConfig cfg, bool value)
        {
            var so = new SerializedObject(cfg);
            var prop = so.FindProperty("unlockedByDefault");
            
            if (prop == null) 
                return false;
            
            if (prop.boolValue == value) 
                return false;
            
            prop.boolValue = value;
            so.ApplyModifiedProperties();
            EditorUtility.SetDirty(cfg);
            return true;
        }
    }
}
