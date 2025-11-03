using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Avastrad.SavingAndLoading
{
    public class BinarySaveAndLoader : ISaveAndLoader
    {
        private readonly string _savePath;

        public BinarySaveAndLoader(string saveFileName = "Save")
        {
            if (string.IsNullOrEmpty(saveFileName))
                throw new NullReferenceException("Save file name can't be empty");
            
            _savePath = Path.Combine(Application.dataPath, saveFileName);
        }
        
        public void Save(object data)
        {
            using (FileStream stream = new FileStream(_savePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }

        public T TryLoad<T>() 
            where T : new()
        {
            if (!Exist())
                return new T();
            
            return Load<T>();
        }

        public T Load<T>()
        {
            if (!Exist())
                throw new NullReferenceException("Save doesnt exist");

            T loadedData;
            using (FileStream stream = new FileStream(_savePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                loadedData = (T) binaryFormatter.Deserialize(stream);
            }
            
            return loadedData;
        }

        public bool Exist() 
            => File.Exists(_savePath);

        public void DeleteSave() 
            => File.Delete(_savePath);
    }
}