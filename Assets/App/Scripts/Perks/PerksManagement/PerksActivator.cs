using System;
using App.Perks.Configs;
using Avastrad.Extensions;

namespace App.Perks.PerksManagement
{
    public class PerksActivator
    {
        private readonly PerksStorage _perksStorage;

        public PerksActivator(PerksStorage perksStorage)
        {
            _perksStorage = perksStorage;
        }

        public void ActivatePerk(PerkCell perkCell)
        {
            if (!_perksStorage.AvailablePerks.Contains(perkCell))
                throw new NullReferenceException($"Available perks doesnt contain requested perk: perk.name[{perkCell.name}], perk.title[{perkCell.Title}]");
            
            perkCell.Perform(this);

            _perksStorage.ActivatePerk(perkCell);
        }

        public void ActivateSpawnPerk(SpawnPerk spawnPerk) 
            => EcsSpawner.Spawn(spawnPerk.Key);
    }
}