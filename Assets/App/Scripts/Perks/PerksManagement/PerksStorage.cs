using System;
using System.Collections.Generic;
using App.Perks.Configs;
using Avastrad.Extensions;
using Random = UnityEngine.Random;

namespace App.Perks.PerksManagement
{
    public class PerksStorage
    {
        private readonly List<PerkCell> _activatedPerks = new();
        private readonly List<PerkCell> _availablePerks = new();
        private readonly List<PerkCell> _globalAvailablePerks = new();
        
        public int CountOfAllAvailablePerks => _availablePerks.Count + _globalAvailablePerks.Count;
        public int CountOfAvailableMainPerks => _availablePerks.Count;
        public IReadOnlyList<PerkCell> AvailablePerks => _availablePerks;
        public IReadOnlyList<PerkCell> ActivatedPerks => _activatedPerks;

        public PerksStorage(IReadOnlyList<PerkCell> initialPerks, IReadOnlyList<PerkCell> initialGlobalPerks)
        {
            _availablePerks.AddRange(initialPerks);
            _globalAvailablePerks.AddRange(initialGlobalPerks);
        }

        public IReadOnlyList<PerkCell> GetRandomPerks(int perksCount, bool withGlobalPerks = true)
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
            
            var perks = new List<PerkCell>();

            var availablePerksBuffer = new List<PerkCell>(_availablePerks);
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

        public bool IsAvailable(PerkCell perkCell) 
            => _availablePerks.Contains(perkCell) || _globalAvailablePerks.Contains(perkCell);

        public void ActivatePerk(PerkCell perkCell)
        {
            _activatedPerks.Add(perkCell);
            _availablePerks.Remove(perkCell);
            _globalAvailablePerks.Remove(perkCell);

            foreach (var childPerk in perkCell.ChildPerks)
            {
                if (_activatedPerks.Contains(childPerk) || _availablePerks.Contains(childPerk))
                    continue;
                
                _availablePerks.Add(childPerk);
            }
        }

        public void ActivateSpawnPerk(SpawnPerk spawnPerk) 
            => EcsSpawner.Spawn(spawnPerk.Key);
    }
}