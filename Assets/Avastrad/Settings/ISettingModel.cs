using System;

namespace Avastrad.Settings
{
    public interface ISettingModel
    {
        public int Priority { get; }
        
        public void Apply();

        public Type GetStateType();
        public ISettingState GetState();
        public void LoadState(ISettingState genericState);
    }
}