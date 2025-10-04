using App.Ecs;
using Unity.Entities;
using UnityEngine;

namespace App
{
    public static class EcsPause
    {
        public static void SetPauseState(bool isPause)
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return;
            }

            var pausableInitialization = world.GetExistingSystemManaged<PausableInitializationSystemGroup>();
            var pausablePhysics = world.GetExistingSystemManaged<PhysicsPausableSimulationGroup>();
            var pausableAfterTransform = world.GetExistingSystemManaged<AfterTransformPausableSimulationGroup>();
            var pausableLate = world.GetExistingSystemManaged<PausableLateSimulationSystemGroup>();
            
            pausableInitialization.Enabled = isPause;
            pausablePhysics.Enabled = isPause;
            pausableAfterTransform.Enabled = isPause;
            pausableLate.Enabled = isPause;
        }
    }
}