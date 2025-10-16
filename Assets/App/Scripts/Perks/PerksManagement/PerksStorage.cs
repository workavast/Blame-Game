using System;
using System.Collections.Generic;
using App.Perks.Configs;
using Random = UnityEngine.Random;

namespace App.Perks.PerksManagement
{
    public class PerksStorage
    {
        private readonly List<PerkCell> _activatedPerks = new();
        private readonly List<PerkCell> _availablePerks = new();

        public int CountOfAvailablePerks => _availablePerks.Count;
        public IReadOnlyList<PerkCell> AvailablePerks => _availablePerks;
        public IReadOnlyList<PerkCell> ActivatedPerks => _activatedPerks;

        public PerksStorage(IReadOnlyList<PerkCell> initialPerks)
        {
            _availablePerks.AddRange(initialPerks);
        }

        public IReadOnlyList<PerkCell> GetRandomPerks(int perksCount)
        {
            if (perksCount > CountOfAvailablePerks)
                throw new InvalidOperationException($"You request more perks than available: requested [{perksCount}], available[{{CountOfAvailablePerks}}]");
            
            var perks = new List<PerkCell>();
            var availablePerksBuffer = new List<PerkCell>(_availablePerks);

            for (int i = 0; i < perksCount; i++)
            {
                var randomPerkIndex = Random.Range(0, availablePerksBuffer.Count);
                var randomPerk = availablePerksBuffer[randomPerkIndex];
                availablePerksBuffer.RemoveAt(randomPerkIndex);
                
                perks.Add(randomPerk);
            }

            return perks;
        }

        public void ActivatePerk(PerkCell perkCell)
        {
            _activatedPerks.Add(perkCell);
            _availablePerks.Remove(perkCell);

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