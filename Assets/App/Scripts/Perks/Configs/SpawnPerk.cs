using App.Perks.PerksManagement;
using UnityEngine;

namespace App.Perks.Configs
{
    [CreateAssetMenu(fileName = nameof(SpawnPerk), menuName = PerksConsts.Path + nameof(SpawnPerk))]
    public class SpawnPerk : PerkConfig
    {
        [SerializeField] private MonoBehaviour ecsPerkPrefab;

        public int Key => ecsPerkPrefab.name.GetHashCode();
        public MonoBehaviour EcsPerkPrefab => ecsPerkPrefab;

        public override void Perform(PerksActivator perksActivator) 
            => perksActivator.ActivateSpawnPerk(this);
    }
}