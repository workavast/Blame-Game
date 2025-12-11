using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace App.Localization
{
    [Serializable]
    public class StringTablesPreloader : IDisposable
    {
        [SerializeField] private StringTablesConfig config;

        private IReadOnlyList<TableReference> TableReferences => config.TableReferences;
        private AsyncOperationHandle? _preloadHandle;

        public void Initialize()
        {
            LocalizationSettings.SelectedLocaleChanged += RePreload;
        }
        
        public void Dispose()
        {
            LocalizationSettings.SelectedLocaleChanged -= RePreload;
        }
        
        public async Task Preload()
        {
            if (config == null)
            {
                Debug.LogWarning("String tables config is null");
                return;
            }

            if (_preloadHandle.HasValue && _preloadHandle.Value.IsValid())
            {
                Debug.LogWarning("You try load string tables when them already loaded");
                return;
            }

            var selectedLocale = LocalizationSettings.SelectedLocale;
            var handle = LocalizationSettings.StringDatabase.PreloadTables(TableReferences.ToList(), selectedLocale);
            _preloadHandle = handle;
            await handle.Task;
        }

        public void Release()
        {
            if (_preloadHandle.HasValue && _preloadHandle.Value.IsValid())
                Addressables.Release(_preloadHandle);
            
            _preloadHandle = null;
        }

        private void RePreload(Locale _)
        {
            Release();
            Preload();
        }
    }
}