using App.EscProviding;
using App.TypeReferencing;
using Avastrad.Settings;
using Avastrad.UI.UiSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Settings
{
    public class SettingsScreen : ScreenBase, IEscListener
    {
        [SerializeField] private TypeReference<ScreenBase> screen;
        [SerializeField] private SettingsWindow settingsWindow;
        [SerializeField] private CloseWarningWindow closeWarningWindow;
        [SerializeField] private Button closeScreenBtn;

        [Inject] private readonly ScreensController _screensController;
        [Inject] private readonly EscProvider _escProvider;

        public override void Initialize()
        {
            settingsWindow.Initialize();
            closeScreenBtn.onClick.AddListener(TryCloseScreen);

            closeWarningWindow.OnClose += CloseScreen;
            
            base.Initialize();
        }
        
        private void OnEnable()
        {
            closeWarningWindow.gameObject.SetActive(false);
            _escProvider.Sub(this);
        }

        private void OnDisable()
        {
            _escProvider.UnSub(this);
        }
        
        public void OnEscPressed() 
            => TryCloseScreen();

        private void TryCloseScreen()
        {
            if (settingsWindow.HasChangedAny())
                closeWarningWindow.gameObject.SetActive(true);
            else
                _screensController.SetScreen(screen.Type);
        }

        private void CloseScreen(bool withSave)
        {
            if (withSave)
                SaveAndClose();
            else
                CancelAndClose();
        }
        
        private void SaveAndClose()
        {
            settingsWindow.ApplySettings();
            _screensController.SetScreen(screen.Type); 
        }

        private void CancelAndClose()
        {
            settingsWindow.ResetSettings();
            _screensController.SetScreen(screen.Type); 
        }
    }
}