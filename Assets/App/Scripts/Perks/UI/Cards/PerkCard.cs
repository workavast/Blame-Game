using System;
using App.Perks.Configs;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace App.Perks.UI.Cards
{
    public class PerkCard : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent title;
        [SerializeField] private LocalizeStringEvent description;
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

            icon.sprite = _perkConfig.Icon;
            title.StringReference = _perkConfig.GetTitle();
            description.StringReference = _perkConfig.GetDescription();
        }
    }
}