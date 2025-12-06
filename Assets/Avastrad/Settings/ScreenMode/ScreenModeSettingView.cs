using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Avastrad.Settings.ScreenMode
{
    public class ScreenModeSettingView : MonoBehaviour, ISettingView
    {
        [SerializeField] private TMP_Dropdown screenModeDropdown;
        [SerializeField] private ScreenModeSettingViewModel viewModel;

        [SerializeField] private LocalizedString fullScreenLocalizedString;
        [SerializeField] private LocalizedString windowLocalizedString;
        
        public void Initialize()
        {
            UpdateView();

            LocalizationSettings.SelectedLocaleChanged += UpdateLocale;
            UpdateLocale(LocalizationSettings.SelectedLocale);
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            screenModeDropdown.onValueChanged.AddListener(Set);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            screenModeDropdown.onValueChanged.RemoveListener(Set);
        }

        private void Set(int value)
        {
            viewModel.Set(screenModeDropdown.value == 0, false);
        }

        private void UpdateView()
        {
            screenModeDropdown.SetValueWithoutNotify(viewModel.IsFullScreen ? 0 : 1);
            screenModeDropdown.RefreshShownValue();
        }
        
        private void UpdateLocale(Locale selectedLocale)
        {
            screenModeDropdown.options[0].text = fullScreenLocalizedString.GetLocalizedString();
            screenModeDropdown.options[1].text = windowLocalizedString.GetLocalizedString();
            screenModeDropdown.RefreshShownValue();
        }
    }
}