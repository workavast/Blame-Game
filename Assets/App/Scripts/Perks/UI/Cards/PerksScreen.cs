using System.Collections.Generic;
using App.Perks.Configs;
using App.UI;
using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.Perks.UI.Cards
{
    public class PerksScreen : DefaultScreen
    {
        [SerializeField] private PerksChooseWindow perksChooseWindow;
        
        [Inject] private readonly ScreensController _screensController;
        
        public int CardsCount => perksChooseWindow.CardsCount;
        
        public override void Initialize()
        {
            perksChooseWindow.OnPerkChoose += TurnOffSelf;
            perksChooseWindow.Initialize();
            base.Initialize();
        }

        public void ShowPerksVariants(IReadOnlyList<PerkConfig> randomPerks) 
            => perksChooseWindow.ShowPerksVariants(randomPerks);
        
        private void TurnOffSelf() 
            => _screensController.ToggleScreen<PerksScreen>(false);
    }
}