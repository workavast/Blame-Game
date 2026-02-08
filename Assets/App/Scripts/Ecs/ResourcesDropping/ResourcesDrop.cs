using App.Ecs.Health.Death;
using App.Ecs.Randomisation;
using App.ResourcesSystem;
using App.ResourcesSystem.ForRun;
using Unity.Collections;
using Unity.Entities;

namespace App.Ecs.ResourcesDropping
{
    [UpdateInGroup(typeof(DropSystemGroup))]
    public abstract partial class ResourceDropSystem<TDropper> : SystemBase
        where TDropper : unmanaged, IComponentData
    {
        protected abstract ResourceType ResourceType { get; }
        
        protected override void OnCreate()
        {
            var query = GetEntityQuery(
                ComponentType.ReadOnly<TDropper>(),
                ComponentType.ReadWrite<RandomHolder>(),
                ComponentType.ReadOnly<DeathFlag>()
            );
            RequireForUpdate(query);
        }

        protected override void OnUpdate()
        {
            if (!ServicesBridge.Exist<ResourcesForRunProvider>())
                return;
            var resourcesForRunProvider = ServicesBridge.Get<ResourcesForRunProvider>();

            var query = GetEntityQuery(
                ComponentType.ReadOnly<TDropper>(),
                ComponentType.ReadWrite<RandomHolder>(),
                ComponentType.ReadOnly<DeathFlag>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var droppers = query.ToComponentDataArray<TDropper>(Allocator.Temp);
            var randomHolders = query.ToComponentDataArray<RandomHolder>(Allocator.Temp);
            
            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var dropper = droppers[i];
                var randomHolder = randomHolders[i];
                
                var amount = GetAmount(dropper, ref randomHolder);
                EntityManager.SetComponentData(entity, randomHolder);

                resourcesForRunProvider.Add(ResourceType, amount);
            }
        }

        protected abstract int GetAmount(TDropper dropper, ref RandomHolder randomHolder);
    }
}