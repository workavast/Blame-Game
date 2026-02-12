using System.IO;
using Avastrad.SavingAndLoading;
using UnityEngine;

namespace App.Saves
{
    public abstract class SaveModule<T>
        where T: new()
    {
        private readonly ISaveAndLoader _saveAndLoader;
        
        /// <param name="filePath"> not full path</param>
        protected SaveModule(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError($"SaveModule [{GetType().Name}]: filePath is empty");
                return;
            }
            
            string fullPath;
            string pathRoot;

            if (Application.isEditor)
                pathRoot = $"{Application.dataPath}/{AppConsts.AppPath}";
            else
                pathRoot = Application.persistentDataPath;
            
            if (filePath.StartsWith(pathRoot)) 
                fullPath = filePath;
            else
            {
                if (filePath[0] == '/') 
                    fullPath = pathRoot + filePath;
                else
                    fullPath = Path.Combine(pathRoot, filePath);;
            }

            if (!filePath.EndsWith(".json")) 
                fullPath += ".json";
            
            _saveAndLoader = new JsonSaveAndLoader(fullPath);
        }

        protected void Save(object data) 
            => _saveAndLoader.Save(data);

        protected T Load() 
            => _saveAndLoader.TryLoad<T>();
        
        protected bool Exist() 
            => _saveAndLoader.Exist();

        protected void DeleteSave()
            => _saveAndLoader.DeleteSave();
    }
}