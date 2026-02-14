using System.Collections.Generic;
using App.Perks.Configs;
using App.ResourcesSystem.ResourcesValues;
using UnityEngine;

namespace App.Unlocks
{
    [CreateAssetMenu(fileName = nameof(UnlockConfig), menuName = UnlocksConsts.Path + nameof(UnlockConfig))]
    public class UnlockConfig : ScriptableObject
    {
        [SerializeField] private PerkConfig perk;
        [SerializeField] private List<UnlockConfig> childUnlocks;
        [SerializeField] private ResourcesValue cost;

        public string Id => perk.Id;
        public PerkConfig Perk => perk;
        public IReadOnlyList<UnlockConfig> ChildUnlocks => childUnlocks;
        public ResourcesValue Cost => cost;
    }
}