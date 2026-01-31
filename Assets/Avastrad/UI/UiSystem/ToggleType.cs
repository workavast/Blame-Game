using System;

namespace Avastrad.UI.UiSystem
{
    public enum ToggleType
    {
        Auto = 0,
        Show = 1,
        Hide = 2
    }

    public static class ToggleTypeExtension
    {
        public static ToggleType Inverted(this ToggleType toggleType)
        {
            return toggleType switch
            {
                ToggleType.Auto => ToggleType.Auto,
                ToggleType.Show => ToggleType.Hide,
                ToggleType.Hide => ToggleType.Show,
                _ => throw new ArgumentOutOfRangeException(nameof(toggleType), toggleType, null)
            };
        }
    }
}