using UnityEngine;
using Zenject;

namespace App.Unlocks.Saves
{
    public class UnlocksSavesInstaller : MonoInstaller
    {
        [SerializeField] private string filePath;
        
        public override void InstallBindings()
        {
            Container.Bind<UnlocksSaveModule>().FromNew().AsSingle().WithArguments(filePath);
            Container.Bind<UnlocksSaveManger>().FromNew().AsSingle();
        }
    }
}