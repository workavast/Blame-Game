using System;
using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using UnityEngine;

namespace App.Localization
{
    [Serializable]
    public class StringTablesPreloadBootstrap : Bootstrap
    {
        [SerializeField] private StringTablesPreloader preloader;

        protected override Task SelfInitialization(CancellationToken cancellationToken)
        {
            preloader.Initialize();
            return preloader.Preload();
        }

        protected override void OnDestroy()
        {
            preloader.Release();
            preloader.Dispose();
            base.OnDestroy();
        }
    }
}