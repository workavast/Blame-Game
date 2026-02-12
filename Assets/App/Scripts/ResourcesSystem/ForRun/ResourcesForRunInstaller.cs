using App.ResourcesSystem.ResourcesValues;
using UnityEngine;
using Zenject;

namespace App.ResourcesSystem.ForRun
{
    public class ResourcesForRunInstaller : MonoInstaller
    {
        [SerializeField] private ResourcesValueConfig resourcesValueConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesForRunProvider>().FromNew().AsSingle().WithArguments(resourcesValueConfig);
        }
    }
}