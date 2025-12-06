using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Bootstraps
{
    public class RootBootstrap : Bootstrap
    {
        public async void Start()
        {
            try
            {
                await Initialize(CancellationToken.None);
            }
            catch (TaskCanceledException e)
            {
                Debug.Log(e);
            }
        }

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
            => Task.CompletedTask;
    }
}