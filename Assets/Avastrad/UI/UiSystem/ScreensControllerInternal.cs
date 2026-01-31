using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.UI.UiSystem
{
    [DisallowMultipleComponent]
    internal class ScreensControllerInternal : MonoBehaviour
    {
        private ScreensRepository _screenRepository;

        public void Initialize()
        {
            _screenRepository = GetComponentInChildren<ScreensRepository>();
            _screenRepository.Initialize();

            foreach (var screen in _screenRepository.Screens) 
                screen.Initialize();
        }
        
        public IReadOnlyList<Type> GetActiveScreens()
        {
            var screens = new List<Type>();

            foreach (var screen in _screenRepository.Screens)
                if (screen.gameObject.activeSelf) 
                    screens.Add(screen.GetType());
            
            return screens;
        }

        public TScreen GetScreen<TScreen>() where TScreen : ScreenBase 
            => (TScreen)GetScreen(typeof(TScreen));

        public ScreenBase GetScreen(Type screenType) 
            => _screenRepository.GetScreen(screenType);

        public void SetScreen(Type screenType, string[] args = null)
        {
            var newScreen = GetScreen(screenType);
            foreach (var screen in _screenRepository.Screens) 
                screen.SetActive(false, args);

            newScreen.SetActive(true, args);
        }
        
        public void SetScreens(IReadOnlyList<Type> screenTypes, string[] args = null)
        {
            foreach (var screen in _screenRepository.Screens)
                if (screen.isActiveAndEnabled && !Contains(screenTypes, screen.GetType()))
                    TryToggleScreen(screen, false, args);

            foreach (var screenType in screenTypes) 
                ToggleScreen(screenType, true, args);
        }
        
        public void ToggleScreen(Type screenType, ToggleType toggleType, string[] args = null)
        {
            switch (toggleType)
            {
                case ToggleType.Auto:
                    ToggleScreen(screenType, args);
                    break;
                case ToggleType.Show:
                    ToggleScreen(screenType, true, args);
                    break;
                case ToggleType.Hide:
                    ToggleScreen(screenType, false, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toggleType), toggleType, null);
            }
        }

        private void ToggleScreen(Type screenType, string[] args = null)
        {
            var screen = _screenRepository.GetScreen(screenType);
            TryToggleScreen(screen, !screen.isActiveAndEnabled, args);
        }

        private void ToggleScreen(Type screenType, bool show, string[] args = null)
        {
            var screen = _screenRepository.GetScreen(screenType);
            TryToggleScreen(screen, show, args);
        }

        private static void TryToggleScreen(ScreenBase screen, bool show, string[] args = null)
        {
            if (screen.isActiveAndEnabled == show) 
                return;
            screen.SetActive(show, args);
        }
        
        private static bool Contains(IReadOnlyList<Type> list, Type value) 
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i] == value)
                    return true;
            
            return false;
        }
    }
}