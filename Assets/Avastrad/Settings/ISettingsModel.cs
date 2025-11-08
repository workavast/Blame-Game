namespace Avastrad.Settings
{
    public interface ISettingsModel
    {
        public int Priority { get; }
        
        public void Apply();
    }
}