using UnityEngine;
using Zenject;

namespace Avastrad.Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsConfigsRepository settingsConfigsRepository;

        public override void InstallBindings()
        {
            var settingsModel = new SettingsRepository(settingsConfigsRepository);
            Container.Bind<SettingsRepository>().FromInstance(settingsModel).AsSingle();
        }
    }
}