using System;
using App.LevelManagement;
using App.Perks.UI;
using UnityEngine;

namespace App.Perks.PerksManagement
{
    public class PerksScreenShower : IDisposable
    {
        private readonly PerksChooseWindow _perksChooseWindow;
        private readonly PerksStorage _perksStorage;
        private readonly ILevelStorageRO _levelStorage;

        public PerksScreenShower(PerksChooseWindow perksChooseWindow, PerksStorage perksStorage,
            ILevelStorageRO levelStorage)
        {
            _perksChooseWindow = perksChooseWindow;
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
            if (_perksStorage.CountOfAvailablePerks <= 0)
                return;

            var perkCardCount = Mathf.Min(_perksChooseWindow.CardsCount, _perksStorage.CountOfAvailablePerks);
            var randomPerks = _perksStorage.GetRandomPerks(perkCardCount);

            _perksChooseWindow.ShowPerksVariants(randomPerks);
        }
    }
}