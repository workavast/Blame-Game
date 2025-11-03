using UnityEngine;

namespace Avastrad.Settings
{
    public class SettingsConfig : ScriptableObject
    {
        [field: SerializeField] public int Priority { get; private set; }
    }
}