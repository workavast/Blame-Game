using Zenject;

namespace App.ExpManagement
{
    public class ExpManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ExpManager>().FromNew().AsSingle();
        }
    }
}