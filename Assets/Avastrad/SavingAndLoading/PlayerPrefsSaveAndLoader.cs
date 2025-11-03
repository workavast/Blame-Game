using System;
using UnityEngine;

namespace Avastrad.SavingAndLoading
{
    public class PlayerPrefsSaveAndLoader : ISaveAndLoader
    {
        private readonly string _saveKey;

        public PlayerPrefsSaveAndLoader(string saveKey = "Save")
        {
            if (string.IsNullOrEmpty(saveKey))
                throw new NullReferenceException("Save key can't be empty");
            
            _saveKey = saveKey;
        }
        
        public void Save(object data)
        {
            var save = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(_saveKey, save);
            PlayerPrefs.Save();
        }

        public T TryLoad<T>() 
            where T : new()
        {
            if (!Exist())
                return new T();

            var save = PlayerPrefs.GetString(_saveKey);
            
            if (string.IsNullOrEmpty(save))
                return new T();

            return JsonUtility.FromJson<T>(save);
        }
        
        public T Load<T>() 
        {
            if (!Exist())
                throw new NullReferenceException("Save doesnt exist");

            var save = PlayerPrefs.GetString(_saveKey);
            
            if (string.IsNullOrEmpty(save))
                throw new NullReferenceException("Save is empty");

            return JsonUtility.FromJson<T>(save);
        }

        public bool Exist() 
            => PlayerPrefs.HasKey(_saveKey);

        public void DeleteSave()
            => PlayerPrefs.DeleteKey(_saveKey);
    }
}