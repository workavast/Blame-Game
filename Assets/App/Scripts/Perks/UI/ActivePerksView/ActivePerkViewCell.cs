using App.Perks.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Perks.UI.ActivePerksView
{
    public class ActivePerkViewCell : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        private PerkConfig _perkConfig;
        
        public void SetPerk(PerkConfig perkConfig)
        {
            _perkConfig = perkConfig;
            icon.sprite = perkConfig.Icon;
        }
        
        public PerkConfig GetPerkDataConfig() 
            => _perkConfig;
    }
}