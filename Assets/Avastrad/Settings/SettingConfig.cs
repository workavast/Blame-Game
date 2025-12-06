using UnityEngine;

namespace Avastrad.Settings
{
    public abstract class SettingConfig : ScriptableObject
    {
        [field: SerializeField] public int Priority { get; private set; }

        public abstract ISettingModel CreateModel();
    }
}