using Zenject;

namespace App.EcsPausing
{
    public class EcsPauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsPause>().FromNew().AsSingle();
        }
    }
}