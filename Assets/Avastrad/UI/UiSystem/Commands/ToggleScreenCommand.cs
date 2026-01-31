using System;
using UnityEngine;

namespace Avastrad.UI.UiSystem.Commands
{
    internal class ToggleScreenCommand : ICommand
    {
        private readonly Type _targetScreenType;
        private readonly ScreensControllerInternal _screensController;
        private readonly ToggleType _toggleType;

        public bool IsExecuted { get; private set; }

        public ToggleScreenCommand(Type targetScreenType, ScreensControllerInternal screensController,
            ToggleType toggleType)
        {
            _targetScreenType = targetScreenType;
            _screensController = screensController;
            _toggleType = toggleType;
        }

        public void Execute()
        {
            if (IsExecuted)
            {
                Debug.LogError("Command is already executed");
                return;
            }

            IsExecuted = true;
            _screensController.ToggleScreen(_targetScreenType, _toggleType);
        }

        public void Undo()
        {
            if (!IsExecuted)
            {
                Debug.LogError("Command is not executed");
                return;
            }

            IsExecuted = false;
            _screensController.ToggleScreen(_targetScreenType, _toggleType.Inverted());
        }
    }
}