using UnityEngine;
using Zenject;

namespace Avastrad.Settings
{
    public class SettingsPresenter : MonoBehaviour
    {
        [Inject] private readonly SettingsModel _settingsModel;

        private ISettingsViewModel[] _settingsViewModels;
        private ISettingsView[] _settingsViews;

        private void Awake()
        {
            _settingsViewModels = GetComponentsInChildren<ISettingsViewModel>(true);
            _settingsViews = GetComponentsInChildren<ISettingsView>(true);

            foreach (var viewModel in _settingsViewModels)
                viewModel.Initialize();

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
