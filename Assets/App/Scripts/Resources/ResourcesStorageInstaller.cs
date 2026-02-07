using Zenject;

namespace App.Resources
{
    public class ResourcesStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesStorage>().FromNew().AsSingle();
        }
    }
}