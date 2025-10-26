using Zenject;

namespace App.ResolutionProviding
{
    public class ResolutionProvidingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ResolutionProvider>().FromNew().AsSingle();
        }
    }
}