using Avastrad.Settings;
using Avastrad.UI.UiSystem;
using UnityEngine;

namespace App.UI
{
    public class SettingsScreen : ScreenBase
    {
        [SerializeField] private SettingsPresenter settingsPresenter;

        public override void Initialize()
        {
            settingsPresenter.Initialize();
            base.Initialize();
        }
    }
}