using System.Linq;
using App.Saves;
using App.Unlocks.Storage;

namespace App.Unlocks.Saves
{
    public class UnlocksSaveModule : SaveModule<UnlocksSaveModel>
    {
        public UnlocksSaveModule(string filePath) : base(filePath)
        {
        }
        
        public void Save(UnlockStorage unlockStorage)
        {
            var unlocks = unlockStorage.GetUnlocks();
            var saveModel = new UnlocksSaveModel()
            {
                unlocks = unlocks.ToArray()
            };
            
            Save(saveModel);
        }
        
        public void Load(UnlockStorage unlockStorage)
        {
            var saveModel = Load();
            if (saveModel.unlocks == null)
                return;
            
            foreach (var unlockId in saveModel.unlocks)
                unlockStorage.Unlock(unlockId);
        }
    }
}