using UnityEngine;
using Zenject;

namespace Avastrad.Settings
{
    public class SettingsWindow : MonoBehaviour
    {
        [Inject] private readonly SettingsModel _settingsModel;
        
        private ISettingViewModel[] _settingsViewModels;
        private ISettingView[] _settingsViews;

        public void Initialize()
        {
            _settingsViewModels = GetComponentsInChildren<ISettingViewModel>(true);
            _settingsViews = GetComponentsInChildren<ISettingView>(true);

            foreach (var viewModel in _settingsViewModels)
                viewModel.Initialize(_settingsModel);

            foreach (var view in _settingsViews)
                view.Initialize(); 
        }
        
        private void OnEnable()
        {
            foreach (var view in _settingsViews)
                view.OnEnabledManual();
        }

        private void OnDisable()
        {
            foreach (var view in _settingsViews)
                view.OnDisabledManual();
        }

        public bool HasChangedAny()
        {
            foreach (var viewModel in _settingsViewModels)
                if (viewModel.HasChanged)
                    return true;

            return false;
        }
        
        public void ApplySettings()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ApplySettings();

            _settingsModel.Apply();
            _settingsModel.Save();
        }

        public void ResetSettings()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ResetSettings();

            _settingsModel.Save();
        }

        public void ResetToDefaults()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ResetToDefault();

            _settingsModel.Apply();
            _settingsModel.Save();
        }
    }
}
