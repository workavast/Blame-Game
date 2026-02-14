using Zenject;

namespace App.ResourcesSystem.Storage
{
    public class ResourcesStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesStorage>().FromNew().AsSingle();
        }
    }
}