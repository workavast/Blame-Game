using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using Avastrad.Settings;
using Zenject;

namespace App.Settings
{
    public class SettingsBootstrap : Bootstrap
    {
        [Inject] private readonly SettingsRepository _settingsRepository;

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
        {
            _settingsRepository.TryLoad();
            _settingsRepository.Apply(true);

            return Task.CompletedTask;
        }
    }
}