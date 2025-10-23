using System.Collections.Generic;
using UnityEngine;

namespace App.Perks.Configs
{
    [CreateAssetMenu]
    public class InitialPerksConfig : ScriptableObject
    {
        [SerializeField] private List<PerkCell> initialPerks;

        public IReadOnlyList<PerkCell> InitialPerks => initialPerks;
    }
}