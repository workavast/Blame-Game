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
            => preloader.Preload();

        protected override void OnDestroy()
        {
            preloader.Release();
            base.OnDestroy();
        }
    }
}