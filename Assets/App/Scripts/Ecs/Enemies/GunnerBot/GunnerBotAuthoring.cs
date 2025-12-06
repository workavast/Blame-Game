using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.GunnerBot
{
    public class GunnerBotAuthoring : MonoBehaviour
    {
        [Header("random offset of hold position range")]
        [SerializeField] private float minOffset;
        [SerializeField] private float maxOffset;
        [Space]
        [Header("Range of the hold position zone, when bot in zone")]
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [Space]
        [Header("Range of the hold position zone, when bot not in zone")]
        [SerializeField] private float minTarget;
        [SerializeField] private float maxTarget;
        
        private class Baker : Baker<GunnerBotAuthoring>
        {
            public override void Bake(GunnerBotAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new GunnerBotTag());
                AddComponent(entity, new GunnerBotOffsetData()
                {
                    MinOffset = authoring.minOffset,
                    MaxOffset = authoring.maxOffset
                });
                AddComponent(entity, new GunnerBotInZoneFlag());
                AddComponent(entity, new GunnerBotOffsetInitializedFlag());
                AddComponent(entity, new GunnerBotData()
                {
                    MinDistanceInternal = authoring.minDistance,
                    MaxDistanceInternal = authoring.maxDistance,
                    
                    MinTargetInternal = authoring.minTarget,
                    MaxTargetInternal = authoring.maxTarget,
                    
                    Offset = Random.Range(0f, 3f)
                });
            }
        }
    }
}