using UnityEngine;
using Zenject;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveInstaller : MonoInstaller
    {
        [SerializeField] private string saveFilePath;
        
        public override void InstallBindings()
        {
            Container.Bind<ResourcesSaveModule>().FromNew().AsSingle().WithArguments(saveFilePath);
            Container.Bind<ResourcesSaveManager>().FromNew().AsSingle();
        }
    }
}