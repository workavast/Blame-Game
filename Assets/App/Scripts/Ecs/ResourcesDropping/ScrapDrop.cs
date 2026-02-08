using App.Ecs.Randomisation;
using App.Resources;
using Unity.Entities;

namespace App.Ecs.ResourcesDropping
{
    public struct ScrapDropper : IComponentData
    {
        public int MinAmount;
        public int MaxAmount;
    }
    
    public partial class DropScrapSystem : ResourceDropSystem<ScrapDropper>
    {
        protected override ResourceType ResourceType => ResourceType.Scrap;

        protected override int GetAmount(ScrapDropper dropper, ref RandomHolder randomHolder) 
            => randomHolder.Random.NextInt(dropper.MinAmount, dropper.MaxAmount + 1);
    }
}