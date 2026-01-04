using App.Audio.Sources;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Health.Death.Sfx
{
    [RequireComponent(typeof(HealthAuthoring))]
    public class DeathSfxAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<AudioPoolRelease> deathSfxRef;
        
        private class Baker : Baker<DeathSfxAuthoring>
        {
            public override void Bake(DeathSfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new DeathSfxData()
                {
                    DeathSfxRef = authoring.deathSfxRef
                });
                
                AddComponent(entity, new DeathSfxHolderInitializeFlag());
            }
        }
    }
}