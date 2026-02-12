using System;
using System.IO;
using UnityEngine;

namespace Avastrad.SavingAndLoading
{
    public class JsonSaveAndLoader : ISaveAndLoader
    {
        private readonly string _savePath;

        public JsonSaveAndLoader(string fullSavePath)
        {
            if (string.IsNullOrEmpty(fullSavePath))
                throw new NullReferenceException("Save path can't be empty");

            _savePath = fullSavePath;
        }
        
        public void Save(object data)
        {
            if (!Exist())
            {
                Debug.Log("Save doesnt exist");
                CreateFile();
            }
            
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

        public void CreateFile()
        {
            var directory = Path.GetDirectoryName(_savePath);
            
            if (!Directory.Exists(directory)) 
                Directory.CreateDirectory(directory);
            
            using (var writer = new StreamWriter(_savePath)) 
                writer.Write("");
        }
    }
}