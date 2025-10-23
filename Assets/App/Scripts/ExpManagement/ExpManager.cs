using App.Ecs.Experience;
using App.LevelUpManagement;

namespace App.ExpManagement
{
    public class ExpManager
    {
        private readonly LevelUpManager _levelUpManager;
        
        public float ExpAmount { get; private set; }
        public float ExpTarget { get; private set; } = 10;

        public float FillPercentage => ExpAmount / ExpTarget;

        public ExpManager(LevelUpManager levelUpManager)
        {
            _levelUpManager = levelUpManager;
        }
        
        private void Update()
        {
            if (EcsSingletons.TryGetSingletonRO<PlayerExp>(out var playerExp))
            {
                ExpAmount = playerExp.Value;

                if (ExpAmount >= ExpTarget)
                {
                    _levelUpManager.LevelUp();
                    IncreaseExpLimit();
                }
            }
        }

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
            ExpTarget *= 2;
        }
    }
}