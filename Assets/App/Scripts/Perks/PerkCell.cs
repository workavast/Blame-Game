using System.Collections.Generic;
using UnityEngine;

namespace App.Perks
{
    [CreateAssetMenu]
    public class PerkCell : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private MonoBehaviour ecsPerkPrefab;
        [SerializeField] private List<PerkCell> childPerks;

        public int Key => ecsPerkPrefab.name.GetHashCode();
        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public MonoBehaviour EcsPerkPrefab => ecsPerkPrefab;
        public IReadOnlyList<PerkCell> ChildPerks => childPerks;
    }
}