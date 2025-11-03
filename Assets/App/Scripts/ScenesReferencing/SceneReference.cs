using App.ScenesReferencing.ReadOnlyAttributeDrawing;
using UnityEngine;

namespace App.ScenesReferencing
{
    [CreateAssetMenu(fileName = "SceneReference", menuName = "App/" + nameof(SceneReference))]
    public partial class SceneReference : ScriptableObject
    {
        [SerializeField, ReadOnly] private int bakedSceneIndex = -1;

        public int SceneIndex => bakedSceneIndex;
    }
}