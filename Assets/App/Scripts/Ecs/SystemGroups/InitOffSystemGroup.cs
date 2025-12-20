using Unity.Entities;
using Unity.Scenes;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SceneSystemGroup))]
    public partial class InitOffSystemGroup : ComponentSystemGroup
    {
        
    }
}