using System;
using System.Collections.Generic;
using App.Perks.Configs;
using App.Perks.PerksManagement;
using UnityEngine;
using Zenject;

namespace App.Perks.UI
{
    public class PerksChooseWindow : MonoBehaviour
    {
        [SerializeField] private List<PerkCard> perkCards;

        [Inject] private readonly PerksActivator _perksActivator;
        
        public int CardsCount => perkCards.Count;

        public event Action OnPerkChoose;

        public void Initialize()
        {
            foreach (var perkCard in perkCards) 
                perkCard.OnActivate += Perform;
        }

        public void ShowPerksVariants(IReadOnlyList<PerkCell> perks)
        {
            if (perks.Count <= 0)
                return;

            foreach (var perkCard in perkCards) 
                perkCard.gameObject.SetActive(false);

            for (var i = 0; i < perks.Count; i++)
            {
                perkCards[i].gameObject.SetActive(true);
                perkCards[i].SetPerk(perks[i]);
            }
        }
        
        private void Perform(PerkCell perkCell)
        {
            _perksActivator.ActivatePerk(perkCell);
            OnPerkChoose?.Invoke();
        }
    }
}