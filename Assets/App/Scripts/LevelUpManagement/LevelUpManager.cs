using App.EcsPausing;
using App.Perks;
using App.Perks.PerksManagement;
using UnityEngine;

namespace App.LevelUpManagement
{
    public class LevelUpManager
    {
        private readonly PerksChooseWindow _perksChooseWindow;
        private readonly PerksManager _perksManager;
        private readonly EcsPause _ecsPause;

        public LevelUpManager(EcsPause ecsPause, PerksManager perksManager, PerksChooseWindow perksChooseWindow)
        {
            _ecsPause = ecsPause;
            _perksManager = perksManager;
            _perksChooseWindow = perksChooseWindow;
        }
        
        public void LevelUp()
        {
            if (_perksManager.CountOfAvailablePerks <= 0)
                return;

            _ecsPause.SetPauseState(false);

            var perkCardCount = Mathf.Min(_perksChooseWindow.CardsCount, _perksManager.CountOfAvailablePerks);
            var randomPerks = _perksManager.GetRandomPerks(perkCardCount);

            _perksChooseWindow.ShowPerksVariants(randomPerks);
        }
    }
}