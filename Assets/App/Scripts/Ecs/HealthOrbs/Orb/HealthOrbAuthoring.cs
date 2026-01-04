using Unity.Entities;
using UnityEngine;

namespace App.Ecs.HealthOrbs.Orb
{
    public class HealthOrbAuthoring : MonoBehaviour
    {
        [SerializeField] private float amount;
        
        private class ExpOrbBaker : Baker<HealthOrbAuthoring>
        {
            public override void Bake(HealthOrbAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new HealthOrbTag());
                AddComponent(entity, new HealthOrbAmount() { Value = authoring.amount });
            }
        }
    }
}