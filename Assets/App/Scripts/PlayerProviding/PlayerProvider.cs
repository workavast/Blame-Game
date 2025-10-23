using System;
using App.Ecs;
using App.Ecs.Player;

namespace App.PlayerProviding
{
    public class PlayerProvider
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public float FillPercentage { get; private set; }
        
        public bool PlayerSpawned { get; private set; }
        public bool IsAlive { get; private set; }
        public bool PlayerDied { get; private set; }

        public event Action OnPlayerDied;
        
        public void UpdateHealth()
        {
            CheckPlayerSpawned();
            UpdateHealthValues();
            CheckPlayerAliveState();
        }

        private void CheckPlayerSpawned()
        {
            if (!PlayerSpawned)
                if (EcsSingletons.Exist<PlayerTag>()) 
                    PlayerSpawned = true;
        }

        private void UpdateHealthValues()
        {
            if (EcsSingletons.TryGetComponentOfSingletonRO<PlayerTag, MaxHealth>(out var maxHealth))
            {
                MaxHealth = maxHealth.Value;

                if (EcsSingletons.TryGetComponentOfSingletonRO<PlayerTag, CurrentHealth>(out var health))
                {
                    CurrentHealth = health.Value;
                    FillPercentage = CurrentHealth / MaxHealth;
                }
                else
                {
                    FillPercentage = CurrentHealth = 0;
                }
            }
            else
            {
                FillPercentage = MaxHealth = CurrentHealth = 0;
            }
        }

        private void CheckPlayerAliveState()
        {
            IsAlive = FillPercentage > 0;

            if (PlayerSpawned)
            {
                if (!IsAlive && !PlayerDied)
                {
                    PlayerDied = !IsAlive;
                    OnPlayerDied?.Invoke();
                }
                else
                {
                    PlayerDied = !IsAlive;
                }
            }
        }
    }
}