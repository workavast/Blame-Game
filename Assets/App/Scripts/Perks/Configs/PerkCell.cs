using System.Collections.Generic;
using App.Perks.PerksManagement;
using UnityEngine;

namespace App.Perks.Configs
{
    public abstract class PerkCell : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<PerkCell> childPerks;

        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public IReadOnlyList<PerkCell> ChildPerks => childPerks;

        public abstract void Perform(PerksActivator perksActivator);
    }
}