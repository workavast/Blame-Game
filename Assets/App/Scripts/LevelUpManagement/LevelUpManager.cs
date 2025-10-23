using App.GamePausing;
using App.Perks;
using App.Perks.PerksManagement;
using UnityEngine;

namespace App.LevelUpManagement
{
    public class LevelUpManager
    {
        private readonly PerksChooseWindow _perksChooseWindow;
        private readonly PerksManager _perksManager;
        private readonly GamePause _gamePause;

        public LevelUpManager(GamePause gamePause, PerksManager perksManager, PerksChooseWindow perksChooseWindow)
        {
            _gamePause = gamePause;
            _perksManager = perksManager;
            _perksChooseWindow = perksChooseWindow;
        }
        
        public void LevelUp()
        {
            if (_perksManager.CountOfAvailablePerks <= 0)
                return;

            _gamePause.SetPauseState(true);

            var perkCardCount = Mathf.Min(_perksChooseWindow.CardsCount, _perksManager.CountOfAvailablePerks);
            var randomPerks = _perksManager.GetRandomPerks(perkCardCount);

            _perksChooseWindow.ShowPerksVariants(randomPerks);
        }
    }
}