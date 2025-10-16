using UnityEngine;

namespace App.LevelManagement.ExpManagement
{
    [CreateAssetMenu(fileName = nameof(ExpConfig), menuName = "App/" + nameof(ExpConfig))]
    public class ExpConfig : ScriptableObject
    {
        [SerializeField] private float initialExpValue;
        [SerializeField] private float scalePerLevel;

        public float InitialExpLevel => initialExpValue;
        public float ScalePerLevel => scalePerLevel;
    }
}