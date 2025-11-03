using Avastrad.Settings;
using UnityEngine;
using Zenject;

namespace App.UI
{
    public class SettingsScreen : MonoBehaviour
    {
        [Inject] private SettingsModel _settingsModel;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}