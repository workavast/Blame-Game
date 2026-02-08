using Zenject;

namespace App.Resources.Storage
{
    public class ResourcesStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesStorage>().FromNew().AsSingle();
        }
    }
}