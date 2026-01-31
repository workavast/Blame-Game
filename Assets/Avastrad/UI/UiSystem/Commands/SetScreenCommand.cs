using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.UI.UiSystem.Commands
{
    internal class SetScreenCommand : ICommand
    {
        private readonly Type _targetScreenType;
        private readonly ScreensControllerInternal _screensController;

        private IReadOnlyList<Type> _lastActiveScreens;

        public bool IsExecuted { get; private set; }

        public SetScreenCommand(Type targetScreenType, ScreensControllerInternal screensController)
        {
            _targetScreenType = targetScreenType;
            _screensController = screensController;
        }

        public void Execute()
        {
            if (IsExecuted)
            {
                Debug.LogError("Command is already executed");
                return;
            }

            IsExecuted = true;
            _lastActiveScreens = new List<Type>(_screensController.GetActiveScreens());
            _screensController.SetScreen(_targetScreenType);
        }

        public void Undo()
        {
            if (!IsExecuted)
            {
                Debug.LogError("Command is not executed");
                return;
            }

            IsExecuted = false;
            _screensController.SetScreens(_lastActiveScreens);
        }
    }
}