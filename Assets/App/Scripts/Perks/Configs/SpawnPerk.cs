using App.Perks.PerksManagement;
using UnityEngine;

namespace App.Perks.Configs
{
    [CreateAssetMenu]
    public class SpawnPerk : PerkCell
    {
        [SerializeField] private MonoBehaviour ecsPerkPrefab;

        public int Key => ecsPerkPrefab.name.GetHashCode();
        public MonoBehaviour EcsPerkPrefab => ecsPerkPrefab;

        public override void Perform(PerksActivator perksActivator) 
            => perksActivator.ActivateSpawnPerk(this);
    }
}