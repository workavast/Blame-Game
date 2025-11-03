using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using Avastrad.Settings;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

namespace App.Bootstraps
{
    public class AppBootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference sceneForLoadingAfterInitialization;
        
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly SettingsModel _settingsModel;
        
        private async void Start()
        {
            await LocalizationSettings.InitializationOperation.Task;
            
            if (SettingsSaver.Exist()) 
                _settingsModel.Load(SettingsSaver.Load());
            _settingsModel.Apply();

            _sceneLoader.LoadScene(sceneForLoadingAfterInitialization.SceneIndex, true); 
        }
    }
}