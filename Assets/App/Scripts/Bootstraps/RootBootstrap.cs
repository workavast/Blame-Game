using System.Threading.Tasks;

namespace App.Bootstraps
{
    public class RootBootstrap : Bootstrap
    {
        public async void Start() 
            => await Initialize();

        protected override Task SelfInitialization() 
            => Task.CompletedTask;
    }
}