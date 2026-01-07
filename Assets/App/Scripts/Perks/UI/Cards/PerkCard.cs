using System;
using App.Perks.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Perks.UI.Cards
{
    public class PerkCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;
        [SerializeField] private Button activateBtn;
        
        private PerkConfig _perkConfig;

        public event Action<PerkConfig> OnActivate;

        private void Awake()
        {
            activateBtn.onClick.AddListener(() => OnActivate?.Invoke(_perkConfig));
        }

        public void SetPerk(PerkConfig perkConfig)
        {
            _perkConfig = perkConfig;

            title.text = _perkConfig.GetTitle();
            description.text = _perkConfig.GetDescription();
            icon.sprite = _perkConfig.Icon;
        }
    }
}