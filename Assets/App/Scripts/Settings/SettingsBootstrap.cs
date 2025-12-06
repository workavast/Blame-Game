using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using Avastrad.Settings;
using Zenject;

namespace App.Settings
{
    public class SettingsBootstrap : Bootstrap
    {
        [Inject] private readonly SettingsModel _settingsModel;

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
        {
            _settingsModel.TryLoad();
            _settingsModel.Apply(true);

            return Task.CompletedTask;
        }
    }
}