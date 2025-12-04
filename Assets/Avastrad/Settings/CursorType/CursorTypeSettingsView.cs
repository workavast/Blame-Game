using UnityEngine;
using UnityEngine.UI;

namespace Avastrad.Settings.CursorType
{
    public class CursorTypeSettingsView : MonoBehaviour, ISettingView
    {
        [SerializeField] private CursorTypeSettingsViewModel viewModel;
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
            => toggle.SetIsOnWithoutNotify(viewModel.CustomCursor);

        private void SetValue(bool value) 
            => viewModel.SetValue(value, false);
    }
}