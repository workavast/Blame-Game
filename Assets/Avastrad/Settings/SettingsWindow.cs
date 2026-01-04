using UnityEngine;
using Zenject;

namespace Avastrad.Settings
{
    public class SettingsWindow : MonoBehaviour
    {
        [Inject] private readonly SettingsRepository _settingsRepository;
        
        private ISettingViewModel[] _settingsViewModels;
        private ISettingView[] _settingsViews;

        public void Initialize()
        {
            _settingsViewModels = GetComponentsInChildren<ISettingViewModel>(true);
            _settingsViews = GetComponentsInChildren<ISettingView>(true);

            foreach (var viewModel in _settingsViewModels)
                viewModel.Initialize(_settingsRepository);

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
                viewModel.SetToModel();

            _settingsRepository.Apply(false);
            _settingsRepository.Save();
        }

        public void ResetSettings()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ResetSetting();

            _settingsRepository.Apply(false);
            _settingsRepository.Save();
        }

        public void ResetToDefaults()
        {
            _settingsRepository.ResetToDefault();
            
            foreach (var viewModel in _settingsViewModels)
                viewModel.LoadModelData();

            _settingsRepository.Apply(false);
            _settingsRepository.Save();
        }
    }
}
