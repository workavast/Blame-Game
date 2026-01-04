using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Orbs
{
    public class OrbAuthoring : MonoBehaviour
    {
        [SerializeField] private float damping;
        
        private class ExpOrbBaker : Baker<OrbAuthoring>
        {
            public override void Bake(OrbAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new OrbTag());
                AddComponent(entity, new OrbDamping() { Value = authoring.damping });
            }
        }
    }
}