using UnityEngine;

namespace App.ScenesReferencing
{
    [CreateAssetMenu(fileName = "SceneReference", menuName = "App/" + nameof(SceneReference))]
    public partial class SceneReference : ScriptableObject
    {
        // Это значение будет "запечено" в редакторе
        [SerializeField, ReadOnly] private int bakedSceneIndex = -1;

        public int SceneIndex => bakedSceneIndex;
    }
}