using System;
using UnityEngine;

namespace App.RollingBands
{
    public class RollingBandsCleanuper : IDisposable
    {
        private readonly Material _rollingBandsMaterial;
        private static int Remap => RollingBandsConsts.RemapPropertyId;
        private static int Timer => RollingBandsConsts.TimerPropertyId;
        
        public RollingBandsCleanuper(Material rollingBandsMaterial)
        {
            _rollingBandsMaterial = rollingBandsMaterial;
        }
        
        public void Dispose()
        {
            _rollingBandsMaterial.SetVector(Remap, Vector2.one);
            _rollingBandsMaterial.SetFloat(Timer, 0);
        }
    }
}