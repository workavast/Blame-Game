using System;
using UnityEngine;
using Zenject;

namespace App.ResolutionProviding
{
    public class ResolutionProvider : IResolutionProviderRO, ITickable
    {
        public Vector2Int Resolution { get; private set; }

        public event Action OnResolutionChanged;

        public ResolutionProvider()
        {
            Resolution = GetResolution();
        }
        
        public void Tick()
        {
            var prevResolution = Resolution;
            Resolution = GetResolution();
           
            if (prevResolution != Resolution)
                OnResolutionChanged?.Invoke();
        }

        private static Vector2Int GetResolution() 
            => new(Screen.width, Screen.height);
    }
}