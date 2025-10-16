using App.Ecs.Experience;
using UnityEngine;

namespace App.LevelManagement.ExpManagement
{
    public class ExpStorage : IExpStorageRO
    {
        public float ExpAmount { get; private set; }
        public float ExpTarget { get; private set; }
        public float ExpPrevTarget { get; private set; }

        public float FillPercentage => ExpAmount / ExpTarget;
        public float FillTargetPercentage => (ExpAmount - ExpPrevTarget) / (ExpTarget - ExpPrevTarget);

        private readonly ExpConfig _config;
        
        public ExpStorage(ExpConfig config)
        {
            _config = config;
            ExpTarget = config.InitialExpLevel;
        }
        
        public bool IsReachExpTarget()
        {
            if (EcsSingletons.TryGetSingletonRO<PlayerExp>(out var playerExp))
            {
                ExpAmount = playerExp.Value;
                if (ExpAmount >= ExpTarget)
                    return true;
            }

            return false;
        }
        
        public void IncreaseExpTarget()
        {
            ExpPrevTarget = ExpTarget;
            ExpTarget *= _config.ScalePerLevel;
        }
    }
}