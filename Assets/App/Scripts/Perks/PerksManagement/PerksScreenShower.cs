using System;
using App.LevelManagement;
using App.Perks.UI;
using Avastrad.UI.UiSystem;
using UnityEngine;

namespace App.Perks.PerksManagement
{
    public class PerksScreenShower : IDisposable
    {
        private readonly ScreensController _screensController;
        private readonly PerksStorage _perksStorage;
        private readonly ILevelStorageRO _levelStorage;

        public PerksScreenShower(ScreensController screensController, PerksStorage perksStorage,
            ILevelStorageRO levelStorage)
        {
            _screensController = screensController;
            _perksStorage = perksStorage;
            _levelStorage = levelStorage;

            _levelStorage.OnLevelUp += TryShowPerksScreen;
        }
        
        public void Dispose()
        {
            _levelStorage.OnLevelUp -= TryShowPerksScreen;
        }

        private void TryShowPerksScreen()
        {
            if (_perksStorage.CountOfAvailableMainPerks <= 0)
                return;

            var perksScreen = _screensController.ToggleScreen<PerksScreen>(true);
            var perkCardCount = Mathf.Min(perksScreen.CardsCount, _perksStorage.CountOfAvailableMainPerks);
            var randomPerks = _perksStorage.GetRandomPerks(perkCardCount, _levelStorage.Level > 1);
            perksScreen.ShowPerksVariants(randomPerks);
        }
    }
}