using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using Zenject;

namespace App.Unlocks.Saves
{
    public class UnlocksSavesBootstrap : Bootstrap
    {
        [Inject] private readonly UnlocksSaveManger _saver;
        
        protected override Task SelfInitialization(CancellationToken cancellationToken)
        {
            _saver.Load();
            return Task.CompletedTask;
        }
    }
}