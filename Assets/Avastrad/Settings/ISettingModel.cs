using System;

namespace Avastrad.Settings
{
    public interface ISettingModel
    {
        public bool HasChanged { get; }
        public int Priority { get; }

        public void Apply();
        public void ResetToDefault();

        public Type GetStateType();
        public ISettingState GetState();
        public void LoadState(ISettingState genericState);
    }
}