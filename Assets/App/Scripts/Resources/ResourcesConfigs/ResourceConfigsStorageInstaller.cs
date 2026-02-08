using UnityEngine;
using Zenject;

namespace App.Resources.ResourcesConfigs
{
    public class ResourceConfigsStorageInstaller : MonoInstaller
    {
        [SerializeField] private ResourcesConfigsStorage resourcesConfigs;

        public override void InstallBindings()
        {
            if (resourcesConfigs == null)
            {
                Debug.LogError("ResourceConfigsStorage is not assigned in the inspector!");
                return;
            }

            Container.BindInstance(resourcesConfigs).AsSingle();
        }
    }
}

