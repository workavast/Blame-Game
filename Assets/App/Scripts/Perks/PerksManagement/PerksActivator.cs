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

        public void ActivatePerk(PerkConfig perkConfig)
        {
            if (!_perksStorage.IsAvailable(perkConfig))
                throw new NullReferenceException($"Available perks doesnt contain requested perk: " +
                                                 $"perk.name[{perkConfig.name}], " +
                                                 $"perk.title[{perkConfig.GetTitle()}]");
            
            perkConfig.Perform(this);

            _perksStorage.ActivatePerk(perkConfig);
        }

        public void ActivateSpawnPerk(SpawnPerk spawnPerk)
        {
            var playerEntity = EcsBridge.GetSingletonEntity<PlayerTag>();
            EcsSpawnBridge.Spawn(spawnPerk.Key, playerEntity);
        }
    }
}