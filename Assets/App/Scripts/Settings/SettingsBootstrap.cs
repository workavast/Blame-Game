using System.Threading.Tasks;
using App.Bootstraps;
using Avastrad.Settings;
using Avastrad.Settings.Save;
using Zenject;

namespace App.Settings
{
    public class SettingsBootstrap : Bootstrap
    {
        [Inject] private readonly SettingsModel _settingsModel;

        protected override Task SelfInitialization()
        {
            _settingsModel.TryLoad();
            _settingsModel.Apply(true);

            return Task.CompletedTask;
        }
    }
}