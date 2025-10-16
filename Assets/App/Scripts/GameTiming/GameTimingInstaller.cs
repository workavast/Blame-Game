using Zenject;

namespace App.GameTiming
{
    public class GameTimingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameTimer>().FromNew().AsSingle();
        }
    }
}