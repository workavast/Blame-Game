using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using UnityEngine;

namespace App.GameTiming
{
    public class GameTimerBootstrap : Bootstrap
    {
        [SerializeField] private GameTimerUpdater gameTimerUpdater;
        
        protected override Task SelfInitialization(CancellationToken cancellationToken)
        {
            gameTimerUpdater.StartTimer();
            return Task.CompletedTask;
        }
    }
}