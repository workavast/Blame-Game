using UnityEngine;
using UnityEngine.UI;

namespace App.Settings.Template
{
    public class TemplateSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private TemplateSettingsViewModel viewModel;
        [SerializeField] private Slider slider;
        
        public void Initialize()
        {
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            slider.onValueChanged.AddListener(SetValue);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            slider.onValueChanged.RemoveListener(SetValue);
        }

        private void UpdateView() 
            => slider.SetValueWithoutNotify(viewModel.Value);

        private void SetValue(float value) 
            => viewModel.SetValue(value, false);
    }
}