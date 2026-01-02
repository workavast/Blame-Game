using System.Collections.Generic;
using App.Perks.Configs;
using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.Perks.UI.Cards
{
    public class PerksScreen : ScreenBase
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

        public void ShowPerksVariants(IReadOnlyList<PerkCell> randomPerks) 
            => perksChooseWindow.ShowPerksVariants(randomPerks);
        
        private void TurnOffSelf() 
            => _screensController.ToggleScreen<PerksScreen>(false);
    }
}