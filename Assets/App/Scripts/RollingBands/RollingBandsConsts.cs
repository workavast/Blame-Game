using UnityEngine;

namespace App.RollingBands
{
    public static class RollingBandsConsts
    {
        public static readonly int RemapPropertyId = Shader.PropertyToID("_Remap");
        public static readonly int TimerPropertyId = Shader.PropertyToID("_UnscaledTime");
    }
}