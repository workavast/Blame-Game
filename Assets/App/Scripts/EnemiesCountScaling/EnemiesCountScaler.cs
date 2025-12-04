using App.Ecs.Enemies.Spawning;
using App.EcsBridges;
using App.EnemiesCountScaling.Configs;
using Unity.Entities;

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
            EcsBridge.TrySetComponentOfSingleton<TSpawnerTag, EnemySpawnCountPerSecond>(
                new EnemySpawnCountPerSecond() { Value = scale });
        }
    }

    public interface IEnemiesScaler
    {
        public void UpdateEnemiesScaling(float timeInMinutes);
    }
}