using UnityEngine;
using UnityEngine.UI;

namespace Avastrad.Settings.VSync
{
    public class VSyncSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private VSyncSettingsViewModel viewModel;
        [SerializeField] private Toggle toggle;
        
        public void Initialize()
        {
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            toggle.onValueChanged.AddListener(SetValue);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            toggle.onValueChanged.RemoveListener(SetValue);
        }

        private void UpdateView() 
            => toggle.SetIsOnWithoutNotify(viewModel.UseVSync);

        private void SetValue(bool isActive) 
            => viewModel.SetValue(isActive, false);
    }
}