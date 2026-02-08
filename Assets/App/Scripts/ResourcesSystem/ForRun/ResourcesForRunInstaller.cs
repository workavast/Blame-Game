using Zenject;

namespace App.ResourcesSystem.ForRun
{
    public class ResourcesForRunInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesForRunProvider>().FromNew().AsSingle();
        }
    }
}