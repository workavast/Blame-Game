using System.Collections.Generic;
using App.GamePausing;
using App.Perks.Configs;
using App.Perks.PerksManagement;
using UnityEngine;
using Zenject;

namespace App.Perks.UI
{
    public class PerksChooseWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform cardsHolder;
        [SerializeField] private List<PerkCard> perkCards;

        [Inject] private readonly PerksActivator _perksActivator;
        [Inject] private readonly GamePause _gamePause;
        
        public int CardsCount => perkCards.Count;

        private void Awake()
        {
            foreach (var perkCard in perkCards) 
                perkCard.OnActivate += Perform;
            Hide();
        }

        public void ShowPerksVariants(IReadOnlyList<PerkCell> perks)
        {
            if (perks.Count <= 0)
                return;

            _gamePause.SetPauseState(true);
            
            cardsHolder.gameObject.SetActive(true);

            foreach (var perkCard in perkCards) 
                perkCard.gameObject.SetActive(false);

            for (var i = 0; i < perks.Count; i++)
            {
                perkCards[i].gameObject.SetActive(true);
                perkCards[i].SetPerk(perks[i]);
            }
        }
        
        private void Hide()
        {
            cardsHolder.gameObject.SetActive(false);
            _gamePause.SetPauseState(false);
        }
        
        private void Perform(PerkCell perkCell)
        {
            _perksActivator.ActivatePerk(perkCell);
            Hide();
        }
    }
}