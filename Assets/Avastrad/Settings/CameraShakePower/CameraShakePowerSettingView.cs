using Avastrad.UI.Elements.SliderExt;
using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    public class CameraShakePowerSettingView : MonoBehaviour, ISettingView
    {
        [SerializeField] private CameraShakePowerSettingViewModel viewModel;
        [SerializeField] private SliderWithStep slider;
        
        public void Initialize()
        {
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            slider.OnValueChanged += SetValue;
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            slider.OnValueChanged -= SetValue;
        }

        private void UpdateView() 
            => slider.SetValue(viewModel.ShakePower, false);

        private void SetValue(float shakePower) 
            => viewModel.SetValue(shakePower, false);
    }
}