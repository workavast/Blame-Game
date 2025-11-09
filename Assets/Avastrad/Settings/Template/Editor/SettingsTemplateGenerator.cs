using System.IO;
using UnityEditor;
using UnityEngine;

namespace Avastrad.Settings.Template.Editor
{
    public class SettingsTemplateGenerator : EditorWindow
    {
        private string _settingsName = "MySettings";
        
        private const string TemplateFolder = "Assets/Avastrad/Settings/Template/";
        private static readonly string DefaultOutputFolder = Application.dataPath + "/Avastrad/Settings";
        private static string _customOutputFolder = DefaultOutputFolder;

        private const string EditorPrefsSaveKey = "SettingsGenerator_OutputPath";
        
        [MenuItem("Tools/Settings Generator")]
        public static void ShowWindow()
        {
            GetWindow<SettingsTemplateGenerator>("Settings Generator");
        }

        private void OnEnable()
        {
            _customOutputFolder = EditorPrefs.GetString(EditorPrefsSaveKey, DefaultOutputFolder);
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(EditorPrefsSaveKey, _customOutputFolder);
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Settings Generator", EditorStyles.boldLabel);
            _settingsName = EditorGUILayout.TextField("Settings Name", _settingsName);
            
            GUILayout.Space(8);
            
            EditorGUILayout.LabelField("Output Folder:", EditorStyles.boldLabel);
            EditorGUILayout.TextField(_customOutputFolder);
         
            GUILayout.Space(8);
            
            if (GUILayout.Button("Choose Output Folder"))
            {
                string selectedPath = EditorUtility.OpenFolderPanel("Select Output Folder", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _customOutputFolder = selectedPath;
                    EditorPrefs.SetString(EditorPrefsSaveKey, _customOutputFolder);
                }
            }
            
            GUILayout.Space(8);
            
            if (GUILayout.Button("Reset Output Folder")) 
                _customOutputFolder = DefaultOutputFolder;

            GUILayout.Space(8);
            
            if (GUILayout.Button("Generate Settings"))
            {
                if (string.IsNullOrWhiteSpace(_settingsName))
                {
                    EditorUtility.DisplayDialog("Error", "Please enter a valid setting name.", "OK");
                    return;
                }
                
                if (!Directory.Exists(_customOutputFolder))
                {
                    EditorUtility.DisplayDialog("Error", "Selected output folder does not exist.", "OK");
                    return;
                }

                GenerateClasses(_settingsName);
                AssetDatabase.Refresh();
            }
        }

        private static void GenerateClasses(string settingsName)
        {
            var outputFolder = $"{_customOutputFolder}/{settingsName}";
            Directory.CreateDirectory(outputFolder);

            Generate(outputFolder, $"{TemplateFolder}/TemplateSettingsConfig.cs.txt", "Template", 
                settingsName, $"{settingsName}SettingsConfig.cs");
            Generate(outputFolder, $"{TemplateFolder}/TemplateSettingsModel.cs.txt", "Template",
                settingsName, $"{settingsName}SettingsModel.cs");
            Generate(outputFolder, $"{TemplateFolder}/TemplateSettingsView.cs.txt", "Template", 
                settingsName, $"{settingsName}SettingsView.cs");
            Generate(outputFolder, $"{TemplateFolder}/TemplateSettingsViewModel.cs.txt", "Template", 
                settingsName, $"{settingsName}SettingsViewModel.cs");
            
            Debug.Log($"Generate {settingsName} in {outputFolder}");
        }

        private static void Generate(string outputFolder, string templatePath, string stringToReplace, string stringReplace, string outputFileName)
        {
            Directory.CreateDirectory(outputFolder);

            var template = File.Exists(templatePath) ? File.ReadAllText(templatePath) : null;
            if (string.IsNullOrEmpty(template))
            {
                EditorUtility.DisplayDialog("Error", "Template files not found or empty.", "OK");
                return;
            }
            
            var result = template.Replace(stringToReplace, stringReplace);

            var path = Path.Combine(outputFolder, outputFileName);
            if (File.Exists(path))
            {
                EditorUtility.DisplayDialog("Error", $"File with this name already exist: [{path}]", "OK");
                return;
            }
            
            File.WriteAllText(path, result);
        }
    }
}
