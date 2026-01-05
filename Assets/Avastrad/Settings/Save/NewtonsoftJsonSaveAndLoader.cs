using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Avastrad.Settings.Save
{
    public class NewtonsoftJsonSaveAndLoader
    {
        private readonly string _savePath;

        public NewtonsoftJsonSaveAndLoader(string savePath)
        {
            if (string.IsNullOrEmpty(savePath))
                throw new NullReferenceException("Save path can't be empty");
            
            _savePath = savePath;
        }
        
        public void Save(object data)
        {
            var directory = Path.GetDirectoryName(_savePath);
            if (!Directory.Exists(directory)) 
                Directory.CreateDirectory(directory);
            
            var save = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
            
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

            if (string.IsNullOrEmpty(save) || JsonIsValid<T>(save))
                return new T();
            
            return JsonConvert.DeserializeObject<T>(save,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
        }
        
        public T Load<T>()
        {
            if (!Exist())
                throw new NullReferenceException("Save doesnt exist");
                
            var save = "";
            using (var reader = new StreamReader(_savePath)) 
                save += reader.ReadToEnd();
        
            if (string.IsNullOrEmpty(save))
                throw new NullReferenceException("Save is empty");

            if (!JsonIsValid<T>(save))
                throw new NullReferenceException("Json is Invalid");
            
            return JsonConvert.DeserializeObject<T>(save,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (_, args) =>
                    {
                        Debug.LogWarning($"JSON Error at path {args.ErrorContext.Path}: {args.ErrorContext.Error.Message}");
                        args.ErrorContext.Handled = true;
                    }
                });
        }

        public bool HasValidSave<TRootType>()
        {
            if (!Exist())
                return false;
                
            var save = "";
            using (var reader = new StreamReader(_savePath)) 
                save += reader.ReadToEnd();
        
            if (string.IsNullOrEmpty(save))
                return false;

            if (!JsonIsValid<TRootType>(save))
                return false;

            return true;
        }
        
        public bool Exist() 
            => File.Exists(_savePath);

        public void DeleteSave()
            => File.Delete(_savePath);
        
        private static bool JsonIsValid<TRootType>(string json)
        {
            try
            {
                var token = JToken.Parse(json);

                if (token is JObject root)
                {
                    if (root.TryGetValue("$type", out var typeJsonValue) && typeJsonValue is JValue)
                    {
                        var typeName = typeJsonValue.ToString();
                        var type = Type.GetType(typeName);
                        
                        return type == typeof(TRootType);    
                    }
                }

                return false;
            }
            catch (JsonReaderException e)
            {
                Debug.LogWarning($"Invalid JSON: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error on JSON validation: {e.Message}");
                return false;
            }
        }
    }
}