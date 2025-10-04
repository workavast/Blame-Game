using Unity.Entities;
using Unity.Mathematics;

namespace App.Ecs
{
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
        }
    }
}