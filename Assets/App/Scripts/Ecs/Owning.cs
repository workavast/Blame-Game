using Unity.Entities;

namespace App.Ecs
{
    public struct Owner : IComponentData
    {
        public Entity Value;
    }
}