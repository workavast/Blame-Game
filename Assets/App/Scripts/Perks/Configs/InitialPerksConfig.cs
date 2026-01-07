using System.Collections.Generic;
using UnityEngine;

namespace App.Perks.Configs
{
    [CreateAssetMenu]
    public class InitialPerksConfig : ScriptableObject
    {
        [SerializeField] private List<PerkConfig> initialPerks;

        public IReadOnlyList<PerkConfig> InitialPerks => initialPerks;
    }
}