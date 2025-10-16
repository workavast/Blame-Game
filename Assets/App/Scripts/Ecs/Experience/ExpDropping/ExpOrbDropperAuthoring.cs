using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Experience.ExpDropping
{
    public class ExpOrbDropperAuthoring : MonoBehaviour
    {
        [SerializeField] private int orbsCount;
        
        private class Baker : Baker<ExpOrbDropperAuthoring>
        {
            public override void Bake(ExpOrbDropperAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ExpOrbDropper() { OrbsCount = authoring.orbsCount });
            }
        }
    }
}