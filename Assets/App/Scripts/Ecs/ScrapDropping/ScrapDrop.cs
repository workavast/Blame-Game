using App.Ecs.Health.Death;
using App.Resources.ForRun;
using Unity.Entities;

namespace App.Ecs.ScrapDropping
{
    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial struct AddScrapSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DeathFlag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var resourcesStorage = ServicesBridge.Get<ResourcesForRunProvider>();
            foreach (var _ in SystemAPI.Query<RefRO<DeathFlag>>()) 
                resourcesStorage.AddScrap(1);
        }
    }
}