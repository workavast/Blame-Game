using System.Threading.Tasks;
using App.Bootstraps;
using UnityEngine.Localization.Settings;

namespace App.Localization
{
    public class LocalizationBoostrap : Bootstrap
    {
        protected override Task SelfInitialization() 
            => LocalizationSettings.InitializationOperation.Task;
    }
}