using System;
using App.ResourcesSystem.Storage;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveManager : IDisposable
    {
        private readonly ResourcesSaveModule _saveModule;
        private readonly ResourcesStorage _resourcesStorage;

        public ResourcesSaveManager(ResourcesSaveModule saveModule, ResourcesStorage resourcesStorage)
        {
            _saveModule = saveModule;
            _resourcesStorage = resourcesStorage;
        }
        
        public void Dispose() 
            => Deactivate();
        
        public void Save()
            => _saveModule.Save(_resourcesStorage);

        public void Load() 
            => _saveModule.Load(_resourcesStorage);
        
        public void Activate() 
            => _resourcesStorage.OnChanged += Save;

        public void Deactivate() 
            => _resourcesStorage.OnChanged -= Save;
    }
}