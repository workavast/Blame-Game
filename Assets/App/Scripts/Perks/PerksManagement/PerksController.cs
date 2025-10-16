using System;
using App.LevelManagement;
using App.Perks.Configs;
using UnityEngine;

namespace App.Perks.PerksManagement
{
    public class PerksController
    {
        private readonly PerksChooseWindow _perksChooseWindow;
        private readonly PerksManager _perksManager;
        private readonly ILevelStorageRO _levelStorage;

        public PerksController(PerksChooseWindow perksChooseWindow, PerksManager perksManager, ILevelStorageRO levelStorage)
        {
            _perksChooseWindow = perksChooseWindow;
            _perksManager = perksManager;
            _levelStorage = levelStorage;

            _levelStorage.OnLevelUp += ShowScreen;
        }

        private void ShowScreen()
        {
            if (_perksManager.CountOfAvailablePerks <= 0)
                return;

            var perkCardCount = Mathf.Min(_perksChooseWindow.CardsCount, _perksManager.CountOfAvailablePerks);
            var randomPerks = _perksManager.GetRandomPerks(perkCardCount);

            _perksChooseWindow.ShowPerksVariants(randomPerks);
        }
        
        public void ActivateSpawnPerk(SpawnPerk spawnPerk) 
            => EcsSpawner.Spawn(spawnPerk.Key);
    }
}