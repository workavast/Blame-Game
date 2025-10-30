using App.Perks.Configs;
using App.Perks.UI;
using UnityEngine;
using Zenject;

namespace App.Perks.PerksManagement
{
    public class PerksManagementInstaller : MonoInstaller
    {
        [SerializeField] private InitialPerksConfig initialPerksConfig;
        [SerializeField] private InitialPerksConfig globalPerksConfig;
        [SerializeField] private PerksChooseWindow perksChooseWindow;

        public override void InstallBindings()
        {
            Container.Bind<PerksStorage>().FromNew().AsSingle().WithArguments(initialPerksConfig.InitialPerks, globalPerksConfig.InitialPerks);
            Container.Bind<PerksScreenShower>().FromNew().AsSingle().WithArguments(perksChooseWindow).NonLazy();
            Container.Bind<PerksActivator>().FromNew().AsSingle();
        }
    }
}