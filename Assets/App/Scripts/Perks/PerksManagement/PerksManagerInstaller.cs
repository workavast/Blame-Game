using App.Perks.Configs;
using UnityEngine;
using Zenject;

namespace App.Perks.PerksManagement
{
    public class PerksManagerInstaller : MonoInstaller
    {
        [SerializeField] private InitialPerksConfig initialPerksConfig;
        [SerializeField] private PerksChooseWindow perksChooseWindow;

        public override void InstallBindings()
        {
            Container.Bind<PerksManager>().FromNew().AsSingle().WithArguments(initialPerksConfig.InitialPerks);
            Container.Bind<PerksController>().FromNew().AsSingle().WithArguments(perksChooseWindow).NonLazy();
        }
    }
}