using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Experience.ExpOrb
{
    public class ExpOrbAuthoring : MonoBehaviour
    {
        [SerializeField] private float expAmount;
        [SerializeField] private float damping;
        
        private class ExpOrbBaker : Baker<ExpOrbAuthoring>
        {
            public override void Bake(ExpOrbAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new ExpOrbTag());
                AddComponent(entity, new ExpOrbAmount() { Value = authoring.expAmount });
                AddComponent(entity, new ExpOrbDamping() { Value = authoring.damping });
                
                AddComponent(entity, new AutoMoveTag());
                AddComponent(entity, new MoveSpeed());
                AddComponent(entity, new MoveDirection());
            }
        }
    }
}