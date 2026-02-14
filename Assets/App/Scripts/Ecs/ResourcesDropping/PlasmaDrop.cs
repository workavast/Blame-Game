using App.Ecs.Randomisation;
using App.ResourcesSystem;
using Unity.Entities;

namespace App.Ecs.ResourcesDropping
{
    public struct PlasmaDropper : IComponentData
    {
        public int MinAmount;
        public int MaxAmount;
    }
    
    public partial class DropPlasmaSystem : ResourceDropSystem<PlasmaDropper>
    {
        protected override ResourceType ResourceType => ResourceType.Plasma;

        protected override int GetAmount(PlasmaDropper dropper, ref RandomHolder randomHolder) 
            => randomHolder.Random.NextInt(dropper.MinAmount, dropper.MaxAmount + 1);
    }
}