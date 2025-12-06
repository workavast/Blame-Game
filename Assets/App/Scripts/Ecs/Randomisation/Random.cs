using Unity.Entities;
using Unity.Mathematics;

namespace App.Ecs.Randomisation
{
    public struct RandomHolderRequiredInitializationFlag : IComponentData, IEnableableComponent
    {

    }
    
    public struct RandomHolder : IComponentData
    {
        public Random Random;
    }
    
    public struct SingletonRandom : IComponentData
    {
        public Random Random;
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct RandomInitializer : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponent<SingletonRandom>(entity);
            state.EntityManager.SetComponentData(entity, new SingletonRandom() { Random = Random.CreateFromIndex(0) });
            
            state.RequireForUpdate<RandomHolderRequiredInitializationFlag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (requiredInitializationFlag, randomHolder) in 
                     SystemAPI.Query<EnabledRefRW<RandomHolderRequiredInitializationFlag>, RefRW<RandomHolder>>())
            {
                var random = SystemAPI.GetSingletonRW<SingletonRandom>();
                randomHolder.ValueRW.Random = new Random(random.ValueRW.Random.NextUInt());
                requiredInitializationFlag.ValueRW = true;
            }
        }
    }
}