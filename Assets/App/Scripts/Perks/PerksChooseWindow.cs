using System.Collections.Generic;
using App.EcsPausing;
using App.Perks.PerksManagement;
using UnityEngine;
using Zenject;

namespace App.Perks
{
    public class PerksChooseWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform cardsHolder;
        [SerializeField] private List<PerkCard> perkCards;

        [Inject] private readonly PerksManager _perksManager;
        [Inject] private readonly EcsPause _ecsPause;
        
        public int CardsCount => perkCards.Count;

        private float _levelTimer;
        
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
            _ecsPause.SetPauseState(true);
        }
        
        private void Perform(PerkCell perkCell)
        {
            _perksManager.ActivatePerk(perkCell);
            Hide();
        }
    }
}