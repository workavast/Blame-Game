using System;
using Avastrad.SavingAndLoading;

namespace Avastrad.Settings
{
    public static class SettingsSaver
    {
#if UNITY_EDITOR
        private static readonly JsonSaveAndLoader JsonSaveAndLoader = new("/Editor/Settings.json");
#else
        private static readonly JsonSaveAndLoader JsonSaveAndLoader = new("/Settings.json");
#endif

        public static bool Exist() 
            => JsonSaveAndLoader.Exist();

        public static SettingsModel Load()
        {
            if (Exist())
                return JsonSaveAndLoader.Load<SettingsModel>();

            throw new NullReferenceException("Doesnt have save");
        }

        public static void Save(SettingsModel settingsModel)
        {
            JsonSaveAndLoader.Save(settingsModel);
        }
        
        public static void Delete()
        {
            JsonSaveAndLoader.DeleteSave();
        }
    }
}