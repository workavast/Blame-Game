namespace App.Unlocks.Graphs
{
    public class UnlockCell
    {
        private UnlockConfig UnlockConfig { get; set; }
        private bool Unlocked { get; set; }
        
        public UnlockCell(UnlockConfig unlockConfig)
        {
            UnlockConfig = unlockConfig;
        }
    }
}