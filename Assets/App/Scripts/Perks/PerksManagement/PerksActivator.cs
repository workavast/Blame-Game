using System;
using App.Ecs.Player;
using App.EcsBridges;
using App.Perks.Configs;

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
            if (!_perksStorage.IsAvailable(perkCell))
                throw new NullReferenceException($"Available perks doesnt contain requested perk: perk.name[{perkCell.name}], perk.title[{perkCell.GetTitle()}]");
            
            perkCell.Perform(this);

            _perksStorage.ActivatePerk(perkCell);
        }

        public void ActivateSpawnPerk(SpawnPerk spawnPerk)
        {
            var playerEntity = EcsBridge.GetSingletonEntity<PlayerTag>();
            EcsSpawnBridge.Spawn(spawnPerk.Key, playerEntity);
        }
    }
}