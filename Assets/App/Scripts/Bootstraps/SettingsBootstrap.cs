using System.Threading.Tasks;
using Avastrad.Settings;
using Zenject;

namespace App.Bootstraps
{
    public class SettingsBootstrap : Bootstrap
    {
        [Inject] private readonly SettingsModel _settingsModel;

        protected override Task SelfInitialization()
        {
            if (SettingsSaver.Exist()) 
                _settingsModel.Load(SettingsSaver.Load());
            _settingsModel.Apply();

            return Task.CompletedTask;
        }
    }
}