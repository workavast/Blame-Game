using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace App.Perks.PerksManagement
{
    public class PerksManager
    {
        private readonly List<PerkCell> _activatedPerks = new();
        private readonly List<PerkCell> _availablePerks = new();

        public int CountOfAvailablePerks => _availablePerks.Count;
        public IReadOnlyList<PerkCell> ActivatedPerks => _activatedPerks;

        public PerksManager(IReadOnlyList<PerkCell> initialPerks)
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
            if (!_availablePerks.Contains(perkCell))
                throw new NullReferenceException($"Available perks doesnt contain requested perk: perk.name[{perkCell.name}], perk.title[{perkCell.Title}]");
            
            EcsSpawner.Spawn(perkCell.Key);
            
            _activatedPerks.Add(perkCell);
            _availablePerks.Remove(perkCell);

            foreach (var childPerk in perkCell.ChildPerks)
            {
                if (_activatedPerks.Contains(childPerk) || _availablePerks.Contains(childPerk))
                    continue;
                
                _availablePerks.Add(childPerk);
            }
        }
    }
}