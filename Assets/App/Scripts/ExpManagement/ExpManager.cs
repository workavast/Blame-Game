using App.Ecs.Experience;

namespace App.ExpManagement
{
    public class ExpManager
    {
        public float ExpAmount { get; private set; }
        public float ExpTarget { get; private set; } = 10;
        public float ExpPrevTarget { get; private set; }

        public float FillPercentage => ExpAmount / ExpTarget;
        public float FillTargetPercentage => (ExpAmount - ExpPrevTarget) / (ExpTarget - ExpPrevTarget);

        public bool IsReachExpLimit()
        {
            if (EcsSingletons.TryGetSingletonRO<PlayerExp>(out var playerExp))
            {
                ExpAmount = playerExp.Value;
                if (ExpAmount >= ExpTarget)
                    return true;
            }

            return false;
        }
        
        public void IncreaseExpLimit()
        {
            ExpPrevTarget = ExpTarget;
            ExpTarget *= 1.35f;
        }
    }
}