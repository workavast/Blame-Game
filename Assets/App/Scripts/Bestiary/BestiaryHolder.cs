using System;
using App.Localization;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace App.Bestiary
{
    public class BestiaryHolder : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject bestiaryPrefabRef;
        [SerializeField] private LoadingConfig loadingConfig;
        [SerializeField] private StringTablesPreloader stringTablesPreloader;

        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private BestiaryManager _bestiaryManager;
        private bool _loadStarted;
        
        private void OnDestroy()
        {
            if (_loadStarted)
                bestiaryPrefabRef.ReleaseAsset();
            stringTablesPreloader.Release();
            stringTablesPreloader.Dispose();
        }

        public void Open()
        {
            _sceneLoader.ShowLoadScreen(loadingConfig.ShowDuration, TryLoad);
        }
        
        public void Close()
        {
            _sceneLoader.ShowLoadScreen(loadingConfig.ShowDuration, () =>
            {
                _bestiaryManager.ToggleVisibility(false);
                _sceneLoader.HideLoadScreen(loadingConfig.HideDuration);
            });
        }

        private async void TryLoad()
        {
            if (_bestiaryManager == null)
            {
                _loadStarted = true;
                var prefabGo = await bestiaryPrefabRef.LoadAssetAsync().Task;
                if (!prefabGo.TryGetComponent<BestiaryManager>(out var bestiaryPrefab))
                    throw new NullReferenceException("Asset ref hasn't target component");

                stringTablesPreloader.Initialize();
                await stringTablesPreloader.Preload();
                _bestiaryManager = Instantiate(bestiaryPrefab, transform);
                _bestiaryManager.Initialize(this);
            }

            _bestiaryManager.ToggleVisibility(true);
            
            _sceneLoader.HideLoadScreen(loadingConfig.HideDuration);
        }
    }
}