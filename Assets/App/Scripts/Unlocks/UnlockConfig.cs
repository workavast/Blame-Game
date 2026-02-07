using System.Collections.Generic;
using App.Perks.Configs;
using UnityEngine;

namespace App.Unlocks
{
    [CreateAssetMenu(fileName = nameof(UnlockConfig), menuName = "Unlocks/" + nameof(UnlockConfig))]
    public class UnlockConfig : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private PerkConfig perk;
        [SerializeField] private List<UnlockConfig> childUnlocks;
        [SerializeField] private int cost;
        
        public string Id => id;
        public PerkConfig Perk => perk;
        public IReadOnlyList<UnlockConfig> ChildUnlocks => childUnlocks;
        public int Cost => cost;
    }
}