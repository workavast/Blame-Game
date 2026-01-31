using System;

namespace Avastrad.UI.UiSystem.Commands
{
    internal class CommandsFactory
    {
        private readonly ScreensControllerInternal _screensController;
        
        public CommandsFactory(ScreensControllerInternal screensController) 
            => _screensController = screensController;
        
        public ICommand SetScreen(Type screenType) 
            => new SetScreenCommand(screenType, _screensController);

        public ICommand ToggleScreen(Type screenType) 
            => new ToggleScreenCommand(screenType, _screensController, ToggleType.Auto);

        public ICommand ToggleScreen(Type screenType, bool show)
        {
            var toggleType = show ? ToggleType.Show : ToggleType.Hide;
            return new ToggleScreenCommand(screenType, _screensController, toggleType);
        }
    }
}