using System.Collections.Generic;
using App.Perks.Configs;
using App.Unlocks.Graphs;

namespace App.Unlocks.Storage
{
    public class UnlockStorage : IUnlocksStorage
    {
        private readonly List<UnlockConfig> _unlocks = new(4);

        private readonly Graph _graph;
        
        public UnlockStorage(UnlocksConfig unlocksConfig)
        {
            _graph = new Graph(unlocksConfig.RootConfigs);
        }
        
        public void Unlock(UnlockConfig unlockConfig)
        {
            _graph.Unlock(unlockConfig);
            _unlocks.Add(unlockConfig);
        }

        public bool Unlocked(PerkConfig perkConfig)
        {
            if (perkConfig.UnlockedByDefault)
                return true;
            
            foreach (var unlock in _unlocks)
                if (unlock.Perk == perkConfig)
                    return true;

            return false;
        }

        public bool Unlocked(UnlockConfig unlockConfig)
        {
            if (unlockConfig.Perk.UnlockedByDefault)
                return true;
            
            foreach (var unlock in _unlocks)
                if (unlock.Id == unlockConfig.Id)
                    return true;

            return false;
        }

        public UnlockState GetState(UnlockConfig unlockConfig)
        {
            if (unlockConfig.Perk.UnlockedByDefault)
                return UnlockState.Unlocked;
            
            return _graph.GetState(unlockConfig);
        }

        public IReadOnlyList<UnlockConfig> GetUnlocks() 
            => _unlocks;
    }
}