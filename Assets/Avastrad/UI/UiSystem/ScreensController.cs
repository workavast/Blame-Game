using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.UI.UiSystem
{
    [DisallowMultipleComponent]
    public class ScreensController : MonoBehaviour
    {
        private ScreensRepository _screenRepository;
        private ScreenBase _activeScreen;

        private void Awake()
        {
            _screenRepository = GetComponentInChildren<ScreensRepository>();
        }

        private void Start()
        {
            _screenRepository.Initialize();
            foreach (var screen in _screenRepository.Screens) 
                screen.Initialize();
        }
        
        public void SetScreen<TScreen>(string[] args = null) where TScreen : ScreenBase 
            => SetScreen(typeof(TScreen), args);

        public void SetScreen(Type screenType, string[] args = null)
        {
            var newScreen = _screenRepository.GetScreen(screenType);
            foreach (var screen in _screenRepository.Screens) 
                screen.SetActive(false, args);

            _activeScreen = newScreen;
            _activeScreen.SetActive(true, args);
        }
        
        public void SetScreens(IReadOnlyList<Type> screenTypes, string[] args = null)
        {
            foreach (var screen in _screenRepository.Screens)
                if (screen.isActiveAndEnabled && !Contains(screenTypes, screen.GetType()))
                    TryToggleScreen(screen, false, args);

            foreach (var screenType in screenTypes) 
                ToggleScreen(screenType, true, args);
        }
        
        public TScreen ToggleScreen<TScreen>(string[] args = null)
            where TScreen : ScreenBase
        {
            var screen = _screenRepository.GetScreen<TScreen>();
            TryToggleScreen(screen, !screen.isActiveAndEnabled, args);
            return screen;
        }
        
        public ScreenBase ToggleScreen(Type screenType, string[] args = null)
        {
            var screen = _screenRepository.GetScreen(screenType);
            TryToggleScreen(screen, !screen.isActiveAndEnabled, args);
            return screen;
        }
        
        public TScreen ToggleScreen<TScreen>(bool show, string[] args = null)
            where TScreen : ScreenBase
        {
            var screen = _screenRepository.GetScreen<TScreen>();
            TryToggleScreen(screen, show, args);
            return screen;
        }
        
        public void ToggleScreen(Type screenType, bool show, string[] args = null)
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