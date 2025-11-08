using System;
using Avastrad.Settings.AntiAliasing;
using Avastrad.Settings.Brightness;
using Avastrad.Settings.CameraShakePower;
using Avastrad.Settings.Fps;
using Avastrad.Settings.Localization;
using Avastrad.Settings.Resolution;
using Avastrad.Settings.ScreenMode;
using Avastrad.Settings.Volume;
using Avastrad.Settings.VSync;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings
{
    [Serializable]
    public class SettingsModel
    {
        [field: SerializeField] public VolumeSettingsModel VolumeSettingsModel { get; private set; }
        [field: SerializeField] public FpsSettingsModel FpsSettingsModel { get; private set; }
        [field: SerializeField] public ScreenModeSettingsModel ScreenModeSettingsModel { get; private set; }
        [field: SerializeField] public ResolutionSettingsModel ResolutionSettingsModel { get; private set; }
        [field: SerializeField] public LocalizationSettingsModel LocalizationSettingsModel { get; private set; }
        [field: SerializeField] public CameraShakePowerSettingsModel CameraShakePowerSettingsModel { get; private set; }
        [field: SerializeField] public VSyncSettingsModel VSyncSettingsModel { get; private set; }
        [field: SerializeField] public BrightnessSettingsModel BrightnessSettingsModel { get; private set; }
        [field: SerializeField] public AntiAliasingSettingsModel AntiAliasingSettingsModel { get; private set; }

        private readonly ISettingsModel[] _allSettings;

        [Inject]
        public SettingsModel(SettingsConfigsRepository configsRepository)
        {
            VolumeSettingsModel = new VolumeSettingsModel(configsRepository.GetConfig<VolumeSettingsConfig>());
            FpsSettingsModel = new FpsSettingsModel(configsRepository.GetConfig<FpsConfig>());
            ScreenModeSettingsModel = new ScreenModeSettingsModel(configsRepository.GetConfig<ScreenModeConfig>());
            ResolutionSettingsModel = new ResolutionSettingsModel(configsRepository.GetConfig<ResolutionSettingsConfig>());
            LocalizationSettingsModel = new LocalizationSettingsModel(configsRepository.GetConfig<LocalizationConfig>());
            CameraShakePowerSettingsModel = new CameraShakePowerSettingsModel(configsRepository.GetConfig<CameraShakePowerSettingsConfig>());
            VSyncSettingsModel = new VSyncSettingsModel(configsRepository.GetConfig<VSyncSettingsConfig>());
            BrightnessSettingsModel = new BrightnessSettingsModel(configsRepository.GetConfig<BrightnessSettingsConfig>());
            AntiAliasingSettingsModel = new AntiAliasingSettingsModel(configsRepository.GetConfig<AntiAliasingSettingsConfig>());
            
            _allSettings = new ISettingsModel[]
            {
                VolumeSettingsModel,
                FpsSettingsModel,
                ScreenModeSettingsModel,
                ResolutionSettingsModel,
                LocalizationSettingsModel,
                CameraShakePowerSettingsModel,
                VSyncSettingsModel,
                BrightnessSettingsModel,
                AntiAliasingSettingsModel
            };
            Array.Sort(_allSettings, (x, y) => -x.Priority.CompareTo(y.Priority));
        }

        public void Load(SettingsModel model)
        {
            VolumeSettingsModel.Load(model.VolumeSettingsModel);
            FpsSettingsModel.Load(model.FpsSettingsModel);
            ScreenModeSettingsModel.Load(model.ScreenModeSettingsModel);
            ResolutionSettingsModel.Load(model.ResolutionSettingsModel);
            LocalizationSettingsModel.Load(model.LocalizationSettingsModel);
            CameraShakePowerSettingsModel.Load(model.CameraShakePowerSettingsModel);
            VSyncSettingsModel.Load(model.VSyncSettingsModel);
            BrightnessSettingsModel.Load(model.BrightnessSettingsModel);
            AntiAliasingSettingsModel.Load(model.AntiAliasingSettingsModel);
        }

        public void Save()
            => SettingsSaver.Save(this);

        public void Apply()
        {
            foreach (var settings in _allSettings)
                settings.Apply();
        }
    }
}