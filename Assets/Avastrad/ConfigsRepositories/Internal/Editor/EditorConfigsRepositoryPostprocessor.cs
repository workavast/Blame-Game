#if UNITY_EDITOR

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Avastrad.ConfigsRepositories.Editor
{
    internal class EditorConfigsRepositoryPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (!importedAssets.Any(IsAsset) && !deletedAssets.Any(IsAsset))
                return;
            
            var configsRepositories = Resources.FindObjectsOfTypeAll<ConfigsRepositoryBase>();
            foreach (var repository in configsRepositories)
            {
                if (!repository.IsAutoFill)
                    continue;

                foreach (var asset in importedAssets)
                {
                    if (IsAssetOfType(asset, repository.TargetType))
                    {
                        Debug.Log($"[{repository.TargetType}] Detected change, refreshing configs...");
                        repository.RefreshRepository();
                        break;
                    }
                }

                foreach (var asset in deletedAssets)
                {
                    if (IsAsset(asset))//If .asset was deleted, well, lets think that it was some config
                    {
                        Debug.Log($"[{repository.TargetType}] Detected deletion, refreshing configs...");
                        repository.ClearNulls();
                        break;
                    }
                }
            }
        }

        private static bool IsAsset(string assetPath)
            => assetPath.EndsWith(".asset");

        private static bool IsAssetOfType(string assetPath, Type type)
        {
            if (!assetPath.EndsWith(".asset"))
                return false;
            
            var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            return asset != null && type.IsInstanceOfType(asset);
        }
    }
}
#endif
