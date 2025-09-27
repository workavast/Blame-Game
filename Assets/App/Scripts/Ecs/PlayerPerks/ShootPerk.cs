using Unity.Entities;

namespace App.Ecs.PlayerPerks
{
    public struct ShootReloadTimer : IComponentData
    {
        public float Timer;
    }
    
    public struct ShootDistanceReaction : IComponentData
    {
        public float Value;
    }
}