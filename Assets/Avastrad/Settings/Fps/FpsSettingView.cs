using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Avastrad.Settings.Fps
{
    public class FpsSettingView : MonoBehaviour, ISettingView
    {
        [SerializeField] private TMP_Dropdown fpsDropdown;
        [SerializeField] private FpsSettingViewModel viewModel;

        private IReadOnlyList<int> FpsOptions => viewModel.FpsOptions;
        private int SelectedOptionIndex => viewModel.SelectedOptionIndex;

        public void Initialize()
        {
            fpsDropdown.ClearOptions();
            foreach (var fps in FpsOptions)
                fpsDropdown.options.Add(new TMP_Dropdown.OptionData(fps + " FPS"));
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            fpsDropdown.onValueChanged.AddListener(SetValue);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            fpsDropdown.onValueChanged.RemoveListener(SetValue);
        }

        private void UpdateView()
        {
            fpsDropdown.SetValueWithoutNotify(SelectedOptionIndex);
            fpsDropdown.RefreshShownValue();
        }

        private void SetValue(int optionIndex)
            => viewModel.Set(optionIndex, false);
    }
}