using App.Perks.Configs;

namespace App.Unlocks.Storage
{
    public interface IReadOnlyUnlocksStorage
    {
        public bool Unlocked(PerkConfig perkConfig);
        public bool Unlocked(UnlockConfig unlockConfig);
        public UnlockState GetState(UnlockConfig unlockConfig);
    }
}