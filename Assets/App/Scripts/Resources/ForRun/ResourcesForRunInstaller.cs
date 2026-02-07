using Zenject;

namespace App.Resources.ForRun
{
    public class ResourcesForRunInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesForRunProvider>().FromNew().AsSingle();
        }
    }
}