using System.Linq;
using App.Saves;
using App.Unlocks.Storage;

namespace App.Unlocks.Saves
{
    public class UnlocksSaveModule : SaveModule<UnlocksStorageState>
    {
        public UnlocksSaveModule(string filePath) : base(filePath)
        {
        }
        
        public void Save(UnlockStorage unlockStorage)
        {
            var unlocks = unlockStorage.GetUnlocks();
            var saveState = new UnlocksStorageState()
            {
                unlocks = unlocks.ToArray()
            };
            
            Save(saveState);
        }
        
        public void Load(UnlockStorage unlockStorage)
        {
            var saveState = Load();
            if (saveState.unlocks == null)
                return;
            
            foreach (var unlockId in saveState.unlocks)
                unlockStorage.Unlock(unlockId);
        }
    }
}