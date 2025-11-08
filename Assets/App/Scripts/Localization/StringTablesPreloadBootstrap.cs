using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Bootstraps;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace App.Localization
{
    [Serializable]
    public class StringTablesPreloadBootstrap : Bootstrap
    {
        [SerializeField] private StringTablesPreloader preloader;

        protected override Task SelfInitialization()
        {
            return preloader.Preload();
        }
        
        private void OnDestroy()
        {
            preloader.Release();
        }
    }
}