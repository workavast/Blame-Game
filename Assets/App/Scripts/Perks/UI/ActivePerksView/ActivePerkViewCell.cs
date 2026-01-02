using App.Perks.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Perks.UI.ActivePerksView
{
    public class ActivePerkViewCell : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        private PerkCell _perkCell;
        
        public void SetPerk(PerkCell perkCell)
        {
            _perkCell = perkCell;
            icon.sprite = perkCell.Icon;
        }
        
        public PerkCell GetPerkDataConfig() 
            => _perkCell;
    }
}