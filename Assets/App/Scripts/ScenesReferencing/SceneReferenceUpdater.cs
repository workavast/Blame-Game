using UnityEditor;

namespace App.ScenesReferencing
{
    [InitializeOnLoad]
    internal static class SceneReferenceUpdater
    {
        static SceneReferenceUpdater()
        {
            EditorBuildSettings.sceneListChanged += () =>
            {
                foreach (var sceneRef in AssetDatabase.FindAssets("t:SceneReference"))
                {
                    var path = AssetDatabase.GUIDToAssetPath(sceneRef);
                    var asset = AssetDatabase.LoadAssetAtPath<SceneReference>(path);
                    asset?.OnValidate();
                    EditorUtility.SetDirty(asset);
                }
            };
        }
    }
}