using System;
using App.ResourcesSystem.Storage;
using UnityEngine;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveManager
    {
        private readonly ResourcesSaveModule _saveModule;
        private readonly ResourcesStorage _resourcesStorage;

        public ResourcesSaveManager(ResourcesSaveModule saveModule, ResourcesStorage resourcesStorage)
        {
            _saveModule = saveModule;
            _resourcesStorage = resourcesStorage;
        }
        
        public void Save()
        {
            Debug.Log("Save resources");
            _saveModule.Save(_resourcesStorage);
        }

        public void Load() 
            => _saveModule.Load(_resourcesStorage);
    }
}