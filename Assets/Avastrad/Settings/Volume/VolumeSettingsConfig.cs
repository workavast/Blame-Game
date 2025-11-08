using UnityEngine;
using UnityEngine.Audio;

namespace Avastrad.Settings.Volume
{
    [CreateAssetMenu(fileName = nameof(VolumeSettingsConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(VolumeSettingsConfig))]
    public class VolumeSettingsConfig : SettingsConfig
    {
        [Space]
        [SerializeField] private AudioMixer audioMixer;
        [Space]
        [SerializeField] private string masterParam = "MasterVolume";
        [SerializeField] private string effectsParam = "EffectsVolume";
        [SerializeField] private string musicParam = "MusicVolume";
        [Space]
        [SerializeField, Range(0, 1)] private float defaultMasterVolume;
        [SerializeField, Range(0, 1)] private float defaultMusicVolume;
        [SerializeField, Range(0, 1)] private float defaultEffectsVolume;

        public AudioMixer AudioMixer => audioMixer;
        public string MasterParam => masterParam;
        public string EffectsParam => effectsParam;
        public string MusicParam => musicParam;
        public float DefaultMasterVolume => defaultMasterVolume;
        public float DefaultMusicVolume => defaultMusicVolume;
        public float DefaultEffectsVolume => defaultEffectsVolume;
    }
}