using System.Collections.Generic;
using UnityEngine;

namespace App.Perks
{
    public class PerksChooseWindow : MonoBehaviour
    {
        [SerializeField] private PerksManager perksManager;
        [SerializeField] private RectTransform cardsHolder;
        [SerializeField] private List<PerkCard> perkCards;
        [SerializeField] private float levelTime;

        private float _levelTimer;
        
        private void Awake()
        {
            foreach (var perkCard in perkCards) 
                perkCard.OnActivate += Perform;
            
            Hide();
        }

        private void Update()
        {
            if (!cardsHolder.gameObject.activeSelf) 
                _levelTimer -= Time.deltaTime;
            
            if (_levelTimer <= 0)
            {
                _levelTimer = levelTime;
                TryShowPerksVariants();
            }
        }

        private void TryShowPerksVariants()
        {
            if (perksManager.CountOfAvailablePerks <= 0)
                return;

            cardsHolder.gameObject.SetActive(true);

            EcsPause.SetPauseState(false);

            var perkCardCount = Mathf.Min(perkCards.Count, perksManager.CountOfAvailablePerks);
            var randomPerks = perksManager.GetRandomPerks(perkCardCount);

            foreach (var perkCard in perkCards) 
                perkCard.gameObject.SetActive(false);

            for (var i = 0; i < randomPerks.Count; i++)
            {
                perkCards[i].gameObject.SetActive(true);
                perkCards[i].SetPerk(randomPerks[i]);
            }
        }

        private void Hide()
        {
            cardsHolder.gameObject.SetActive(false);
            EcsPause.SetPauseState(true);
        }
        
        private void Perform(PerkCell perkCell)
        {
            perksManager.ActivatePerk(perkCell);
            Hide();
        }
    }
}