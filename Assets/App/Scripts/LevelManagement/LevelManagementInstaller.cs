using Zenject;

namespace App.LevelManagement
{
    public class LevelManagementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelStorage>().FromNew().AsSingle();
        }
    }
}