using App.Audio.Ambience;
using App.Audio.Settings;
using Avastrad.Settings.Volume;
using UnityEngine;
using Zenject;

namespace App.Audio
{
    public class AudioProjectInstaller : MonoInstaller
    {
        [SerializeField] private VolumeSettingsConfig volumeSettingsConfig;
        
        public override void InstallBindings()
        {
            BindAmbience();
        }

        private void BindAmbience()
        {
            var ambienceManagerHolder = new GameObject() { name =  nameof(AmbienceManager)};
            var ambienceManager = ambienceManagerHolder.AddComponent<AmbienceManager>();

            Container.Bind<AmbienceManager>().FromInstance(ambienceManager).AsSingle();
            Container.Bind<SettingsAudioApplier>().FromNew().AsSingle().WithArguments(volumeSettingsConfig).NonLazy();
        }
    }
}