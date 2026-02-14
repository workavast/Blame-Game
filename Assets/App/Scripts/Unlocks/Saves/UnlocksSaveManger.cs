using App.Unlocks.Storage;

namespace App.Unlocks.Saves
{
    public class UnlocksSaveManger
    {
        private readonly UnlocksSaveModule _saveModule;
        private readonly UnlockStorage _unlockStorage;

        public UnlocksSaveManger(UnlocksSaveModule saveModule, UnlockStorage unlockStorage)
        {
            _saveModule = saveModule;
            _unlockStorage = unlockStorage;
        }
        
        public void Save() 
            => _saveModule.Save(_unlockStorage);

        public void Load() 
            => _saveModule.Load(_unlockStorage);
    }
}