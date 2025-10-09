using Zenject;

namespace App.GamePausing.EcsPausing
{
    public class EcsPauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsPause>().FromNew().AsSingle();
        }
    }
}