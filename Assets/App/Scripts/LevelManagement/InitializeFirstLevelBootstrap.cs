using System.Threading.Tasks;
using App.Bootstraps;
using Zenject;

namespace App.LevelManagement
{
    public class InitializeFirstLevelBootstrap : Bootstrap
    {
        [Inject] private readonly LevelStorage _levelStorage;

        protected override Task SelfInitialization()
        {
            _levelStorage.LevelUp();
            return Task.CompletedTask;
        }
    }
}