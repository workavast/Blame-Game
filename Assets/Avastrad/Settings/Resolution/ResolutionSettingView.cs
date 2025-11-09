using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Avastrad.Settings.Resolution
{
    public class ResolutionSettingView : MonoBehaviour, ISettingView
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private ResolutionSettingViewModel viewModel;

        private IReadOnlyList<InspectorResolution> Resolutions => viewModel.Resolutions;
        private int SelectedResolutionIndex => viewModel.SelectedResolutionIndex;

        public void Initialize()
        {
            resolutionDropdown.ClearOptions();

            var currentResolutionIndex = 0;
            for (var i = 0; i < Resolutions.Count; i++)
            {
                var resolution = Resolutions[i];
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData($"{resolution.Width} x {resolution.Height}"));

                if (resolution.Width == Screen.currentResolution.width && resolution.Height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }

            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            resolutionDropdown.onValueChanged.AddListener(Set);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            resolutionDropdown.onValueChanged.RemoveListener(Set);
        }

        private void Set(int resolutionIndex) 
            => viewModel.Set(resolutionIndex, false);

        private void UpdateView()
        {
            resolutionDropdown.SetValueWithoutNotify(SelectedResolutionIndex);
            resolutionDropdown.RefreshShownValue();
        }
    }
}