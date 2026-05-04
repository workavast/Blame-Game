using UnityEngine;
using Zenject;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveInstaller : MonoInstaller
    {
        [SerializeField] private string saveFilePath;
        
        public override void InstallBindings()
        {
            Container.Bind<ResourcesSaveManager>().FromNew().AsSingle().WithArguments(saveFilePath);
        }
    }
}