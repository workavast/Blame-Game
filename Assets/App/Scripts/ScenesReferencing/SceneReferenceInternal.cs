#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.ScenesReferencing
{
    public partial class SceneReference
    {
        [SerializeField] private SceneAsset sceneAsset;
        
        private void OnValidate()
        {
            if (sceneAsset != null)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                bakedSceneIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);

                if (bakedSceneIndex == -1)
                {
                    Debug.LogWarning($"Сцена {sceneAsset.name} не добавлена в Build Settings!");
                }
            }
            else
            {
                bakedSceneIndex = -1;
            }
        }
    }
}
#endif