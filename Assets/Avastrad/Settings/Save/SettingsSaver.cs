using System;
using UnityEngine;

namespace Avastrad.Settings.Save
{
    public static class SettingsSaver
    {
#if UNITY_EDITOR
        private static readonly NewtonsoftJsonSaveAndLoader NewtonsoftJsonSaveAndLoader =
            new(Application.persistentDataPath + "/Editor/Settings.json");
#else
        private static readonly NewtonsoftJsonSaveAndLoader NewtonsoftJsonSaveAndLoader =
            new(Application.persistentDataPath + "/Settings.json");
#endif

        public static bool Exist() 
            => NewtonsoftJsonSaveAndLoader.Exist();

        public static bool HasValidSave<T>()
            => NewtonsoftJsonSaveAndLoader.HasValidSave<T>();
        
        public static T Load<T>()
        {
            if (HasValidSave<T>())
                return NewtonsoftJsonSaveAndLoader.Load<T>();

            throw new NullReferenceException("Doesnt have valid save");
        }

        public static void Save<T>(T data) 
            => NewtonsoftJsonSaveAndLoader.Save(data);

        public static void Delete() 
            => NewtonsoftJsonSaveAndLoader.DeleteSave();
    }
}