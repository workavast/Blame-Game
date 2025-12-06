using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Audio.Ambience.Effects;
using App.Bootstraps;
using UnityEngine;

namespace App.Audio.Ambience
{
    public class AmbienceBootstrap : Bootstrap
    {
        [SerializeField] private AmbienceInitializer ambienceInitializer;
        [SerializeField] private List<AmbienceEffectorToggler> effectorTogglers; 

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
        {
            ambienceInitializer.Init();
            foreach (var effectorToggler in effectorTogglers)
                effectorToggler.Init();
            
            return Task.CompletedTask;
        }
    }
}