using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Perks
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

            title.text = _perkCell.Title;
            description.text = _perkCell.Description;
            icon.sprite = _perkCell.Icon;
        }
    }
}