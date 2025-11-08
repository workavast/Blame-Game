using System;
using UnityEngine;

namespace Avastrad.ConfigsRepositories
{
    public abstract partial class ConfigsRepository<TConfig> 
        where TConfig : ScriptableObject
    {
#if UNITY_EDITOR
        internal override bool IsAutoFill => configsRepositoryCell.AutoFill;
        internal override Type TargetType => typeof(TConfig);

        internal sealed override void RefreshRepository()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            var newConfigs = Resources.FindObjectsOfTypeAll<TConfig>();
            Configs.Clear();
            foreach (var config in newConfigs)
            {
                if (!Configs.Contains(config))
                    Configs.Add(config);
                else
                    Debug.LogWarning($"Duplicate of the config: [{config.GetType()}]");
            }

            Sort();
        }

        internal sealed override void ClearNulls()
        {
            var hasNull = false;
            
            for (int i = 0; i < Configs.Count; i++)
            {
                if (Configs[i] == null)
                {
                    hasNull = true;
                    Configs.RemoveAt(i);
                    i--;
                }
            }
            
            if (hasNull)
                UnityEditor.EditorUtility.SetDirty(this);
        }
        
        internal override void Sort() 
            => Configs.Sort(Comparison);
#endif
    }
}