using System;
using App.Perks.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Perks.UI
{
    public class PerkCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;
        [SerializeField] private Button activateBtn;
        
        private PerkCell _perkCell;

        public event Action<PerkCell> OnActivate;

        private void Awake()
        {
            activateBtn.onClick.AddListener(() => OnActivate?.Invoke(_perkCell));
        }

        public void SetPerk(PerkCell perkCell)
        {
            _perkCell = perkCell;

            title.text = _perkCell.GetTitle();
            description.text = _perkCell.GetDescription();
            icon.sprite = _perkCell.Icon;
        }
    }
}