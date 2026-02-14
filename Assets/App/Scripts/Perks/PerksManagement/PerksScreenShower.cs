using System;
using App.LevelManagement;
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
            var perksScreen = _screensController.GetScreen<PerksScreen>();

            var withGlobalPerks = _levelStorage.Level > 1; //if it is't initial level -> use global perks
            var perkCardsCount = 0;
            if (_levelStorage.Level > 1)
                perkCardsCount = Mathf.Min(perksScreen.CardsCount, _perksStorage.CountOfAllAvailablePerks);
            else
                perkCardsCount = Mathf.Min(perksScreen.CardsCount, _perksStorage.CountOfAvailableMainPerks);

            if (perkCardsCount <= 0)
            {
                Debug.Log("No perks available");
                return;
            }
            
            var randomPerks = _perksStorage.GetRandomPerks(perkCardsCount, withGlobalPerks);
            _screensController.ToggleScreen<PerksScreen>(true);
            perksScreen.ShowPerksVariants(randomPerks);
        }
    }
}