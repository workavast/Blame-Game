using Avastrad.Settings;
using Avastrad.Settings.CameraShakePower;

namespace App.CameraShaking
{
    public class CameraShakeSettingProvider
    {
        public float ShakePower => _cameraShakePowerSettingsModel.ShakePower;
        
        private readonly CameraShakePowerSettingModel _cameraShakePowerSettingsModel;
        
        public CameraShakeSettingProvider(SettingsRepository settingsRepository)
        {
            _cameraShakePowerSettingsModel = settingsRepository.GetSettingModel<CameraShakePowerSettingModel>();
        }
    }
}