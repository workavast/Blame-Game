using System;
using Avastrad.ConfigsRepositories.InspectorButton;

namespace Avastrad.ConfigsRepositories
{
    public abstract partial class ConfigsRepositoryBase
    {
#if UNITY_EDITOR
        internal abstract bool IsAutoFill { get; }
        internal abstract Type TargetType { get; }

        [Button]
        internal abstract void RefreshRepository();
        [Button]
        internal abstract void ClearNulls();
        [Button]
        internal abstract void Sort();
#endif
    }
}