using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace App.RollingBands
{
    public class RollingBandsInstaller : MonoInstaller
    {
        [SerializeField] private Material rollingBandsMaterial;
        [SerializeField] private RollingBandsConfig config;
        [Space]
        [SerializeField] private UniversalRendererData rendererData;
        [SerializeField] private string rollingBandsName;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RollingBandsTimeUpdater>().FromNew().AsSingle().WithArguments(rollingBandsMaterial).NonLazy();
            Container.Bind<RollingBandsVisibilityChanger>().FromNew().AsSingle().WithArguments(config);
            Container.Bind<RollingBandsToggler>().FromNew().AsSingle().WithArguments(rendererData, rollingBandsName).NonLazy();
        }
    }
}