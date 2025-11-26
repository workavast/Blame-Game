using App.Ecs.Enemies.Spawning;
using App.EnemiesCountScaling.Configs;
using Unity.Entities;
using UnityEngine;

namespace App.EnemiesCountScaling
{
    public class EnemiesCountScaler<TSpawnerTag> : IEnemiesScaler
        where TSpawnerTag : unmanaged, IComponentData
    {
        private readonly EnemiesCountScalerConfig _config;
        
        public EnemiesCountScaler(EnemiesCountScalerConfig config)
        {
            _config = config;
        }

        public void UpdateEnemiesScaling(float timeInMinutes)
        {
            var scale = _config.GetCountPerSecond(timeInMinutes);
            if (!EcsSingletons.TrySetComponentOfSingleton<TSpawnerTag, EnemySpawnCountPerSecond>(
                    new EnemySpawnCountPerSecond() { Value = scale }))
            {
                Debug.LogError($"Cant find singleton with target tag: [{typeof(TSpawnerTag)}]");
            }
        }
    }

    public interface IEnemiesScaler
    {
        public void UpdateEnemiesScaling(float timeInMinutes);
    }
}