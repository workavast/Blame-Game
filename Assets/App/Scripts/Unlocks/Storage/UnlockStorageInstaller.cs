using UnityEngine;
using Zenject;

namespace App.Unlocks.Storage
{
    public class UnlockStorageInstaller : MonoInstaller
    {
        [SerializeField] private UnlocksConfig unlocksConfig; 
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnlockStorage>().FromNew().AsSingle().WithArguments(unlocksConfig);
            Container.Bind<Unlocker>().FromNew().AsSingle();
        }
    }
}