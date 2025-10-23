using UnityEngine;
using Zenject;

namespace App.Perks.PerksManagement
{
    public class PerksManagerInstaller : MonoInstaller
    {
        [SerializeField] private InitialPerksConfig initialPerksConfig;

        public override void InstallBindings()
        {
            Container.Bind<PerksManager>().FromNew().AsSingle().WithArguments(initialPerksConfig.InitialPerks);
        }
    }
}