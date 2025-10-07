using App.Ecs;
using App.Ecs.Experience;
using App.Views;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Authorings
{
    public class ExpConsumeZoneAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<CleanupView> viewPrefab;
        
        private class Baker : Baker<ExpConsumeZoneAuthoring>
        {
            public override void Bake(ExpConsumeZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new ExpConsumeZoneTag());
                AddComponent(entity, new ViewPrefabHolder() { Prefab = authoring.viewPrefab });
            }
        }
    }
}