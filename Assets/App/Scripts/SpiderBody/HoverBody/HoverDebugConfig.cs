using UnityEngine;

namespace App.SpiderBody.HoverBody
{
    [CreateAssetMenu(fileName = nameof(HoverDebugConfig), menuName = "App/" + nameof(HoverDebugConfig))]
    public class HoverDebugConfig : ScriptableObject
    {
        [SerializeField] private HoverDebugData initialInBuildDebugData;
        [Space]
        [SerializeField] private HoverDebugData initialInEditorDebugData;

        public HoverDebugData GetInitialDebugData()
        {
            if (Application.isEditor)
                return initialInEditorDebugData;
            else
                return initialInBuildDebugData;
        }
    }
}