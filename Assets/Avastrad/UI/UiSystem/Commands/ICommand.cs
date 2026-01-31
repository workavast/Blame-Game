namespace Avastrad.UI.UiSystem.Commands
{
    internal interface ICommand
    {
        public bool IsExecuted { get; }

        public void Execute();
        public void Undo();
    }
}