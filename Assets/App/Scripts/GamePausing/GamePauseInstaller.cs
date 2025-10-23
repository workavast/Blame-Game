using Zenject;

namespace App.GamePausing
{
    public class GamePauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GamePause>().FromNew().AsSingle();
        }
    }
}