using System;
using System.IO;
using UnityEngine;

namespace Avastrad.SavingAndLoading
{
    public class JsonSaveAndLoader : ISaveAndLoader
    {
        private readonly string _savePath;

        public JsonSaveAndLoader(string savePath = "/Save.json")
        {
            if (string.IsNullOrEmpty(savePath))
                throw new NullReferenceException("Save path can't be empty");

            if (savePath[0] != '/')
                Debug.LogWarning("Save path started not from /");
            
            _savePath = Application.persistentDataPath + savePath;
        }
        
        public void Save(object data)
        {
            var save = JsonUtility.ToJson(data);
            using (var writer = new StreamWriter(_savePath)) 
                writer.Write(save);
        }

        public T TryLoad<T>()
            where T : new()
        {
            if (!Exist())
                return new T();
                
            var save = "";
            using (var reader = new StreamReader(_savePath)) 
                save += reader.ReadLine();

            if (string.IsNullOrEmpty(save))
                return new T();

            return JsonUtility.FromJson<T>(save);
        }
        
        public T Load<T>()
        {
            if (!Exist())
                throw new NullReferenceException("Save doesnt exist");
                
            var save = "";
            using (var reader = new StreamReader(_savePath)) 
                save += reader.ReadLine();
        
            if (string.IsNullOrEmpty(save))
                throw new NullReferenceException("Save is empty");
        
            return JsonUtility.FromJson<T>(save);
        }
        
        public bool Exist() 
            => File.Exists(_savePath);

        public void DeleteSave()
            => File.Delete(_savePath);
    }
}