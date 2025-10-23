using App.EnemiesCountScaling.Configs;
using UnityEngine;
using Zenject;

namespace App.EnemiesCountScaling
{
    public class EnemiesCountScalersHolderInstaller : MonoInstaller
    {
        [SerializeField] private EnemiesCountScalersConfig config;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemiesCountScalersHolder>().FromNew().AsSingle().WithArguments(config);
        }
    }
}