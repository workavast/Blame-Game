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
            var fixedBeforeTransformPause = world.GetExistingSystemManaged<FixedBeforePhysicsPauseGroup>();
            var pausablePhysics = world.GetExistingSystemManaged<PhysicsPausableSimulationGroup>();
            var beforeTransformPause = world.GetExistingSystemManaged<BeforeTransformPauseSimulationGroup>();
            var pausableAfterTransform = world.GetExistingSystemManaged<AfterTransformPausableSimulationGroup>();
            var pausableLate = world.GetExistingSystemManaged<PausableLateSimulationSystemGroup>();

            fixedBeforeTransformPause.Enabled = isPause;
            pausableInitialization.Enabled = isPause;
            pausablePhysics.Enabled = isPause;
            beforeTransformPause.Enabled = isPause;
            pausableAfterTransform.Enabled = isPause;
            pausableLate.Enabled = isPause;
        }
    }
}