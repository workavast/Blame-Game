namespace App.LevelManagement.ExpManagement
{
    public interface IExpStorageRO
    {
        public float ExpAmount { get; }
        public float ExpTarget { get; }
        public float ExpPrevTarget { get; }

        public float FillPercentage => ExpAmount / ExpTarget;
        public float FillTargetPercentage => (ExpAmount - ExpPrevTarget) / (ExpTarget - ExpPrevTarget);

        public bool IsReachExpTarget();
    }
}