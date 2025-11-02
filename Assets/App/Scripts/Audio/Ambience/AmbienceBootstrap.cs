using System.Collections.Generic;
using App.Audio.Ambience.Effects;
using UnityEngine;

namespace App.Audio.Ambience
{
    public class AmbienceBootstrap : MonoBehaviour
    {
        [SerializeField] private AmbienceInitializer ambienceInitializer;
        [SerializeField] private List<AmbienceEffectorToggler> effectorTogglers; 

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            ambienceInitializer.Init();
            foreach (var effectorToggler in effectorTogglers)
                effectorToggler.Init();
        }
    }
}