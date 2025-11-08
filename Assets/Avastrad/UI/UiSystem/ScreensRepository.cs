using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.UI.UiSystem
{
    [DisallowMultipleComponent]
    public class ScreensRepository : MonoBehaviour
    {
        private readonly Dictionary<Type, ScreenBase> _screens = new();
    
        public IEnumerable<ScreenBase> Screens => _screens.Values;

        public void Initialize()
        {
            var screens = GetComponentsInChildren<ScreenBase>(true);
            foreach (var screen in screens) 
                _screens.Add(screen.GetType(), screen);
        }

        public TScreen GetScreen<TScreen>() 
            where TScreen : ScreenBase
        {
            if (!_screens.TryGetValue(typeof(TScreen), out var screen))
            {
                Debug.LogWarning($"Error: invalid parameter: {typeof(TScreen)}");
                return default;
            }

            return (TScreen)screen;
        }
        
        public ScreenBase GetScreen(Type screenType) 
        {
            if (!_screens.TryGetValue(screenType, out var screen))
            {
                Debug.LogWarning($"Error: invalid parameter: {screenType}");
                return default;
            }

            return screen;
        }
    }
}