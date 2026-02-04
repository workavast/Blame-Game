using System.Collections.Generic;
using App.Perks.PerksManagement;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Perks.Configs
{
    public abstract class PerkConfig : ScriptableObject
    {
        [SerializeField] protected LocalizedString title;
        [SerializeField] protected LocalizedString description;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<PerkConfig> childPerks;

        public Sprite Icon => icon;
        public IReadOnlyList<PerkConfig> ChildPerks => childPerks;
        
        public string GetTitleStr()
        {
            if (title == null || title.IsEmpty)
                return "None";
            
            return title.GetLocalizedString(GetTitleParams());
        }

        public string GetDescriptionStr()
        {
            if (title == null || title.IsEmpty)
                return "None";
            
            return description.GetLocalizedString(GetDescriptionParams());
        }

        public LocalizedString GetTitle()
        {
            if (title == null || title.IsEmpty)
                return default;
            
            return title;
        }
        
        public LocalizedString GetDescription()
        {
            if (title == null || title.IsEmpty)
                return default;
            
            return description;
        }
        
        public abstract void Perform(PerksActivator perksActivator);
        
        protected virtual object[] GetTitleParams() 
            => null;
        
        protected virtual object[] GetDescriptionParams() 
            => null;
    }
}