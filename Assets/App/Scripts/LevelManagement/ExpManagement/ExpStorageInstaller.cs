using UnityEngine;
using Zenject;

namespace App.LevelManagement.ExpManagement
{
    public class ExpStorageInstaller : MonoInstaller
    {
        [SerializeField] private ExpConfig config;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ExpStorage>().FromNew().AsSingle().WithArguments(config);
        }
    }
}