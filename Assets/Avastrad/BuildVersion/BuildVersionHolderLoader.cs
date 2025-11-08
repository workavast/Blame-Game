using System.IO;
using UnityEditor;
using UnityEngine;

namespace Avastrad.BuildVersion
{
    public static class BuildVersionHolderLoader
    {
        public static BuildVersionHolder Load()
        {
            var json = LoadJson();
            var versionHolder = JsonUtility.FromJson<BuildVersionHolder>(json.text);
            Resources.UnloadAsset(json);
            return versionHolder;
        }
        
#if UNITY_EDITOR
        public static void Save(BuildVersionHolder buildVersionHolder, bool refreshAssetDatabase)
        {
            var json = LoadJson();
            
            var text = JsonUtility.ToJson(buildVersionHolder);
            File.WriteAllText(AssetDatabase.GetAssetPath(json), text);
            EditorUtility.SetDirty(json);
            Resources.UnloadAsset(json);

            if (refreshAssetDatabase)
                AssetDatabase.Refresh();
        }
#endif

        private static TextAsset LoadJson()
        {
#if UNITY_EDITOR
            var buildVersionConfigurationPath =
                Path.Combine(Application.dataPath, "Avastrad/BuildVersion/Resources/BuildVersionConfiguration.json");
            if (!File.Exists(buildVersionConfigurationPath))
            {
                using (var fs = File.Create(buildVersionConfigurationPath))
                {
                    byte[] buffer = { (byte)'{', (byte)'}' };
                    fs.Write(buffer);
                }
            }      
#endif
            
            return Resources.Load<TextAsset>("BuildVersionConfiguration");
        }
    }
}