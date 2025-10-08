using App.Ecs;

namespace App.PlayerProviding
{
    public class PlayerProvider
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public float FillPercentage { get; private set; }
        
        public void UpdateHealth()
        {
            if (EcsSingletons.TryGetComponentOfSingletonRO<PlayerTag, MaxHealth>(out var maxHealth))
            {
                MaxHealth = maxHealth.Value;
                
                if (EcsSingletons.TryGetComponentOfSingletonRO<PlayerTag, CurrentHealth>(out var health))
                    CurrentHealth = health.Value;
                else
                    MaxHealth = CurrentHealth = 0;
            }
            else
            {
                MaxHealth = CurrentHealth = 0;
            }

            FillPercentage = CurrentHealth / MaxHealth;
        }
    }
}