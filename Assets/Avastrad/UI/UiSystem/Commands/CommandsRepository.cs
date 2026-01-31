using System;
using System.Collections.Generic;

namespace Avastrad.UI.UiSystem.Commands
{
    internal class CommandsRepository
    {
        private readonly Stack<ICommand> _executedCommands;
        private readonly Stack<ICommand> _undoCommands;

        public int ExecutedCommandsCount => _executedCommands.Count;
        public int UndoCommandsCount => _undoCommands.Count;

        public event Action OnChange;
        
        public CommandsRepository(int executedCommandsCapacity, int undoCommandsCapacity)
        {
            _executedCommands = new Stack<ICommand>(executedCommandsCapacity);
            _undoCommands = new Stack<ICommand>(undoCommandsCapacity);
        }
        
        public void UndoCommand()
        {
            if (_executedCommands.Count <= 0)
                return;

            var command = _executedCommands.Pop();
            command.Undo();
            _undoCommands.Push(command);
            OnChange?.Invoke();
        }
        
        public void RedoCommand()
        {
            if (_undoCommands.Count <= 0)
                return;
            
            var command = _undoCommands.Pop();
            command.Execute();
            OnChange?.Invoke();
        }

        public void Clear()
        {
            _executedCommands.Clear();
            _undoCommands.Clear();
            OnChange?.Invoke();
        }

        public void ExecuteCommand(ICommand command)
        {
            if (command.IsExecuted)
            {
                return;
            }
            
            command.Execute();
            _executedCommands.Push(command);
            _undoCommands.Clear();
            OnChange?.Invoke();
        }
    }
}