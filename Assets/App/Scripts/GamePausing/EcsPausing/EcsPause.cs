using App.Ecs;
using Unity.Entities;
using UnityEngine;

namespace App.GamePausing.EcsPausing
{
    public class EcsPause
    {
        private int _pauseRequestCount;
        
        public void SetPauseState(bool isPause)
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return;
            }

            if (isPause)
                _pauseRequestCount++;
            else
                _pauseRequestCount--;

            if (_pauseRequestCount > 1)
                return;

            if (_pauseRequestCount < 0)
            {
                _pauseRequestCount = 0;
                Debug.LogWarning("Ypu try unset pause ECS when it already unpaused");
                return;
            }
            
            var pausableInitialization = world.GetExistingSystemManaged<PausableInitializationSystemGroup>();
            var fixedBeforeTransformPause = world.GetExistingSystemManaged<FixedBeforePhysicsPauseGroup>();
            var pausablePhysics = world.GetExistingSystemManaged<PhysicsPausableSimulationGroup>();
            var beforeTransformPause = world.GetExistingSystemManaged<BeforeTransformPauseSimulationGroup>();
            var pausableAfterTransform = world.GetExistingSystemManaged<AfterTransformPausableSimulationGroup>();
            var pausableLate = world.GetExistingSystemManaged<PausableLateSimulationSystemGroup>();

            fixedBeforeTransformPause.Enabled = !isPause;
            pausableInitialization.Enabled = !isPause;
            pausablePhysics.Enabled = !isPause;
            beforeTransformPause.Enabled = !isPause;
            pausableAfterTransform.Enabled = !isPause;
            pausableLate.Enabled = !isPause;
        }
    }
}