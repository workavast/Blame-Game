using Zenject;

namespace App.GameTiming
{
    public class GameTimerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameTimer>().FromNew().AsSingle();
        }
    }
}