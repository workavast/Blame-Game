using Zenject;

namespace App.PlayerProviding
{
    public class PlayerProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerProvider>().FromNew().AsSingle();
        }
    }
}