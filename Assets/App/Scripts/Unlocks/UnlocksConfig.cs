using System.Collections.Generic;
using UnityEngine;

namespace App.Unlocks
{
    [CreateAssetMenu(fileName = nameof(UnlocksConfig), menuName = "Unlocks/" + nameof(UnlocksConfig))]
    public class UnlocksConfig : ScriptableObject
    {
        [SerializeField] private List<UnlockConfig> rootConfigs;
        
        public IReadOnlyList<UnlockConfig> RootConfigs => rootConfigs;
    }
}