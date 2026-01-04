using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Experience.ExpOrb
{
    public class ExpOrbAuthoring : MonoBehaviour
    {
        [SerializeField] private float expAmount;
        
        private class ExpOrbBaker : Baker<ExpOrbAuthoring>
        {
            public override void Bake(ExpOrbAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new ExpOrbTag());
                AddComponent(entity, new ExpOrbAmount() { Value = authoring.expAmount });
            }
        }
    }
}