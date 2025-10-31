using UnityEngine;
using Zenject;

namespace App.RollingBands
{
    public class RollingBandsTimeUpdater : ITickable
    {
        private static readonly int UnscaledTime = Shader.PropertyToID("_UnscaledTime");
        private readonly Material _material;
        
        public RollingBandsTimeUpdater(Material material)
        {
            _material = material;
        }

        public void Tick()
        {
            _material.SetFloat(UnscaledTime, Time.unscaledTime);
        }
    }
}