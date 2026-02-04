using System;
using System.Collections.Generic;
using App.LevelManagement;
using App.Perks.Configs;
using App.Perks.UI;
using App.Perks.UI.Cards;
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

            var withGlobalPerks = _levelStorage.Level > 1; //if it is't initial level -> use global perks
            var perkCardsCount = 0;
            if (_levelStorage.Level > 1)
                perkCardsCount = Mathf.Min(perksScreen.CardsCount, _perksStorage.CountOfAllAvailablePerks);
            else
                perkCardsCount = Mathf.Min(perksScreen.CardsCount, _perksStorage.CountOfAvailableMainPerks);

            var randomPerks = _perksStorage.GetRandomPerks(perkCardsCount, withGlobalPerks);
            perksScreen.ShowPerksVariants(randomPerks);
        }
    }
}