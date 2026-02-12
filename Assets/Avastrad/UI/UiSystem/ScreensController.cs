using System;
using Avastrad.UI.UiSystem.Commands;
using UnityEngine;

namespace Avastrad.UI.UiSystem
{
    [DisallowMultipleComponent]
    public class ScreensController : MonoBehaviour
    {
        private readonly CommandsRepository _commandsRepository = new(8, 8);
        private ScreensControllerInternal _screensControllerInternal;
        private CommandsFactory _commandsFactory;

        private void Awake()
        {
            _screensControllerInternal = GetComponentInChildren<ScreensControllerInternal>();
            _screensControllerInternal.Initialize();
            
            _commandsFactory = new CommandsFactory(_screensControllerInternal);
        }
        
        public TScreen GetScreen<TScreen>(string[] args = null) where TScreen : ScreenBase 
            => _screensControllerInternal.GetScreen<TScreen>();

        public void SetScreen<TScreen>()
            where TScreen : ScreenBase 
        {
            var command = _commandsFactory.SetScreen(typeof(TScreen));
            _commandsRepository.ExecuteCommand(command);
        }
        
        public void SetScreen(Type screenType, string[] args = null)
        {
            var command = _commandsFactory.SetScreen(screenType);
            _commandsRepository.ExecuteCommand(command);
        }

        public void ToggleScreen(Type screenType, string[] args = null)
        {
            var command = _commandsFactory.ToggleScreen(screenType);
            _commandsRepository.ExecuteCommand(command);
        }

        public void ToggleScreen(Type screenType, bool show, string[] args = null)
        {
            var command = _commandsFactory.ToggleScreen(screenType, show);
            _commandsRepository.ExecuteCommand(command);
        }

        public TScreen ToggleScreen<TScreen>(bool show, string[] args = null)
            where TScreen : ScreenBase
        {
            var screenType = typeof(TScreen);
            
            var command = _commandsFactory.ToggleScreen(screenType, show);
            _commandsRepository.ExecuteCommand(command);
            
            return _screensControllerInternal.GetScreen<TScreen>();
        }

        public void Revert()
        {
            _commandsRepository.UndoCommand();
        }
    }
}