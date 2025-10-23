using UnityEngine;
using Zenject;

namespace App.EscProviding
{
    public class EcsProvidingInstaller : MonoInstaller
    {
        [SerializeField] private EscProvider escProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<EscProvider>().FromInstance(escProvider).AsSingle();
        }
    }
}