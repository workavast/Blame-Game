using System;
using System.Collections.Generic;
using App.Perks.Configs;
using App.Unlocks;
using App.Unlocks.Storage;
using Random = UnityEngine.Random;

namespace App.Perks.PerksManagement
{
    public class PerksStorage
    {
        private readonly IReadOnlyUnlocksStorage _unlocks;
        private readonly List<PerkConfig> _activatedPerks = new();
        private readonly List<PerkConfig> _availablePerks = new();
        private readonly List<PerkConfig> _globalAvailablePerks = new();
        
        public int CountOfAllAvailablePerks => _availablePerks.Count + _globalAvailablePerks.Count;
        public int CountOfAvailableMainPerks => _availablePerks.Count;
        public IReadOnlyList<PerkConfig> AvailablePerks => _availablePerks;
        public IReadOnlyList<PerkConfig> ActivatedPerks => _activatedPerks;

        public event Action OnActivePerksChanged;

        public PerksStorage(IReadOnlyList<PerkConfig> initialPerks, IReadOnlyList<PerkConfig> initialGlobalPerks, 
            IReadOnlyUnlocksStorage unlocks)
        {
            _unlocks = unlocks;
            
            AddIfUnlocked(_availablePerks, initialPerks, _unlocks);
            AddIfUnlocked(_globalAvailablePerks, initialGlobalPerks, _unlocks);
        }

        public IReadOnlyList<PerkConfig> GetRandomPerks(int perksCount, bool withGlobalPerks = true)
        {
            if (withGlobalPerks)
            {
                if (perksCount > CountOfAllAvailablePerks)
                    throw new InvalidOperationException($"You request more perks than available: requested [{perksCount}], " +
                                                        $"available[{CountOfAllAvailablePerks}]");                
            }
            else
            {
                if (perksCount > CountOfAvailableMainPerks)
                    throw new InvalidOperationException($"You request more perks than available: requested [{perksCount}], " +
                                                        $"available[{CountOfAvailableMainPerks}]");
            }
            
            var perks = new List<PerkConfig>();

            var availablePerksBuffer = new List<PerkConfig>(_availablePerks);
            if (withGlobalPerks) 
                availablePerksBuffer.AddRange(_globalAvailablePerks);

            for (int i = 0; i < perksCount; i++)
            {
                var randomPerkIndex = Random.Range(0, availablePerksBuffer.Count);
                var randomPerk = availablePerksBuffer[randomPerkIndex];
                availablePerksBuffer.RemoveAt(randomPerkIndex);
                
                perks.Add(randomPerk);
            }

            return perks;
        }

        public bool IsAvailable(PerkConfig perkConfig) 
            => _availablePerks.Contains(perkConfig) || _globalAvailablePerks.Contains(perkConfig);

        public void ActivatePerk(PerkConfig perkConfig)
        {
            _activatedPerks.Add(perkConfig);
            _availablePerks.Remove(perkConfig);
            _globalAvailablePerks.Remove(perkConfig);

            foreach (var childPerk in perkConfig.ChildPerks)
            {
                if (_activatedPerks.Contains(childPerk) || _availablePerks.Contains(childPerk))
                    continue;

                AddIfUnlocked(_availablePerks, childPerk, _unlocks);
            }

            OnActivePerksChanged?.Invoke();
        }

        private static void AddIfUnlocked(List<PerkConfig> perksList, IReadOnlyList<PerkConfig> newPerks, IReadOnlyUnlocksStorage unlocks)
        {
            foreach (var newPerk in newPerks)
            {
                if (unlocks.Unlocked(newPerk)) 
                    perksList.Add(newPerk);    
            }
        }
        
        private static void AddIfUnlocked(List<PerkConfig> perksList, PerkConfig newPerk, IReadOnlyUnlocksStorage unlocks)
        {
            if (unlocks.Unlocked(newPerk)) 
                perksList.Add(newPerk);
        }
    }
}