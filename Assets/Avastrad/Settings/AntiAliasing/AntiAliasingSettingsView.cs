using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    public class AntiAliasingSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private AntiAliasingSettingsViewModel viewModel;
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private LocalizedString noneLocalizedString;
   
        private IReadOnlyList<AntialiasingMode> AntiAliasingTypes => viewModel.AntiAliasingModes;
        private int SelectedAntiAliasingIndex => viewModel.SelectedAntiAliasingIndex;
        
        public void Initialize()
        {
            LocalizationSettings.SelectedLocaleChanged += FillDropdown;
            FillDropdown();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            dropdown.onValueChanged.AddListener(Set);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            dropdown.onValueChanged.RemoveListener(Set);
        }
        
        private void Set(int dropDownIndex) 
            => viewModel.SetValue(dropDownIndex, false);
        
        private void UpdateView()
        {
            dropdown.SetValueWithoutNotify(SelectedAntiAliasingIndex);
            dropdown.RefreshShownValue();
        }
        
        private void FillDropdown(Locale _) 
            => FillDropdown();

        private void FillDropdown()
        {
            dropdown.ClearOptions();

            for (var i = 0; i < AntiAliasingTypes.Count; i++)
            {
                var antiAliasingType = AntiAliasingTypes[i];
                dropdown.options.Add(new TMP_Dropdown.OptionData(GetShortName(antiAliasingType)));
            }

            dropdown.value = viewModel.SelectedAntiAliasingIndex;
            dropdown.RefreshShownValue();

            UpdateView();
        }
        
        private string GetShortName(AntialiasingMode antialiasingMode)
        {
            return antialiasingMode switch
            {
                AntialiasingMode.None => noneLocalizedString.GetLocalizedString(),
                AntialiasingMode.FastApproximateAntialiasing => "FAA",
                AntialiasingMode.SubpixelMorphologicalAntiAliasing => "SMAA",
                AntialiasingMode.TemporalAntiAliasing => "TAA",
                _ => throw new ArgumentOutOfRangeException(nameof(antialiasingMode), antialiasingMode, null)
            };
        }
    }
}