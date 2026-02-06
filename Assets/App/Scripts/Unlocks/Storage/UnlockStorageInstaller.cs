using UnityEngine;
using Zenject;

namespace App.Unlocks.Storage
{
    public class UnlockStorageInstaller : MonoInstaller
    {
        [SerializeField] private UnlocksConfig unlocksConfig; 
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UnlockStorage>().FromNew().AsSingle().WithArguments(unlocksConfig);
        }
    }
}