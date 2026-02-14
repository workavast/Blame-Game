using System.Collections.Generic;
using App.Perks.Configs;
using App.Unlocks.Graphs;

namespace App.Unlocks.Storage
{
    public class UnlockStorage : IReadOnlyUnlocksStorage
    {
        private readonly List<string> _unlocks = new(4);

        private readonly Graph _graph;
        
        public UnlockStorage(UnlocksConfig unlocksConfig)
        {
            _graph = new Graph(unlocksConfig.RootConfigs);
        }
        
        public void Unlock(string unlockId)
        {
            _graph.Unlock(unlockId);
            _unlocks.Add(unlockId);
        }

        public bool Unlocked(PerkConfig perkConfig)
        {
            if (perkConfig.UnlockedByDefault)
                return true;
            
            foreach (var unlockId in _unlocks)
                if (unlockId == perkConfig.Id)
                    return true;

            return false;
        }

        public bool Unlocked(UnlockConfig unlockConfig)
        {
            if (unlockConfig.Perk.UnlockedByDefault)
                return true;
            
            foreach (var unlockId in _unlocks)
                if (unlockId == unlockConfig.Id)
                    return true;

            return false;
        }

        public UnlockState GetState(UnlockConfig unlockConfig)
        {
            if (unlockConfig.Perk.UnlockedByDefault)
                return UnlockState.Unlocked;
            
            return _graph.GetState(unlockConfig);
        }

        public IReadOnlyList<string> GetUnlocks() 
            => _unlocks;
    }
}