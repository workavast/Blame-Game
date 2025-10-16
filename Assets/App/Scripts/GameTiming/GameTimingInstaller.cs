using Zenject;

namespace App.GameTiming
{
    public class GameTimingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameTimer>().FromNew().AsSingle();
        }
    }
}