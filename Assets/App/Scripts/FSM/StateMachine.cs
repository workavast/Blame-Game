using System;
using System.Collections.Generic;

namespace App.FSM
{
    public class StateMachine : IDisposable
    {
        private readonly Dictionary<Type, StateBase> _states;
        private StateBase _activeState;

        public Type ActiveState => _activeState.GetType();
        public event Action<Type> OnStateSwitched;
        
        public StateMachine(List<StateBase> states)
        {
            _states = new Dictionary<Type, StateBase>();
            foreach (var state in states)
            {
                state.SetFsm(this);
                _states.Add(state.GetType(), state);
            }
        }

        public void Init<TState>(string[] args = null) 
            where TState: StateBase
        {
            _activeState = _states[typeof(TState)];
            _activeState.Enter(args);
        }
        
        public void ManualUpdate() 
            => _activeState.ManualUpdate();

        public void ManualFixedUpdate()
            => _activeState.ManualFixedUpdate();

        public void SetState<TState>(string[] args = null) 
            where TState : StateBase
        {
            var type = typeof(TState);
            
            if(_activeState.GetType() == type)
                return;
            
            _activeState.Exit();
            _activeState = _states[type];
            _activeState.Enter(args);
            OnStateSwitched?.Invoke(type);
        }

        public bool IsActive<TState>()
            where TState : StateBase
        {
            return ActiveState == typeof(TState);
        }

        public void Dispose()
        {
            foreach (var state in _states.Values) 
                state.Dispose();
        }
    }
}