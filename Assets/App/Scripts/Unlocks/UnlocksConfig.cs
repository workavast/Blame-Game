using System.Collections.Generic;
using UnityEngine;

namespace App.Unlocks
{
    [CreateAssetMenu(fileName = nameof(UnlocksConfig), menuName = UnlocksConsts.Path + nameof(UnlocksConfig))]
    public class UnlocksConfig : ScriptableObject
    {
        [SerializeField] private List<UnlockConfig> rootConfigs;
        
        public IReadOnlyList<UnlockConfig> RootConfigs => rootConfigs;

        public bool GetConfig(string unlockId, out UnlockConfig config)
        {
            foreach (var rootConfig in RootConfigs)
                if (GetUnlockConfig(rootConfig, unlockId, out config))
                    return true;

            config = default;
            return false;
        }

        private static bool GetUnlockConfig(UnlockConfig currentUnlock, string targetId, out UnlockConfig config)
        {
            if (currentUnlock.Id == targetId)
            {
                config = currentUnlock;
                return true;
            }

            foreach (var child in currentUnlock.ChildUnlocks)
                if (GetUnlockConfig(child, targetId, out config))
                    return true;

            config = default;
            return false;
        }
    }
}