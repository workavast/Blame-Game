using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Bootstraps
{
    public abstract class Bootstrap : MonoBehaviour
    {
        [SerializeField] private List<Bootstrap> childBootstraps;

        protected async Task Initialize()
        {
            await SelfInitialization();
            await InitializeChildren();
        }

        protected abstract Task SelfInitialization();
        
        private async Task InitializeChildren()
        {
            foreach (var childBootstrap in childBootstraps) 
                await childBootstrap.Initialize();
        }
    }
}