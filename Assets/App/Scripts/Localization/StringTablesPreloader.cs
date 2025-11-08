using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace App.Localization
{
    [Serializable]
    public class StringTablesPreloader
    {
        [SerializeField] private StringTablesConfig config;

        private IReadOnlyList<TableReference> TableReferences => config.TableReferences;

        public async Task Preload()
        {
            if (config == null)
            {
                Debug.LogWarning("String tables config is null");
                return;
            }
            
            var selectedLocale = LocalizationSettings.SelectedLocale;
            var handle = LocalizationSettings.StringDatabase.PreloadTables(TableReferences.ToList(), selectedLocale);
            await handle.Task;
        }

        public void Release()
        {
            if (config == null)
            {
                Debug.LogWarning("String tables config is null");
                return;
            }
            
            foreach (var tableReference in TableReferences)
                LocalizationSettings.AssetDatabase.ReleaseTable(tableReference);
        }
    }
}