using System;

namespace App.FSM
{
    public abstract class StateBase : IDisposable
    {
        protected StateMachine _fsm;
        
        public abstract void Enter(string[] args);
        public abstract void Exit();
        public abstract void ManualUpdate();
        public abstract void ManualFixedUpdate();

        public void SetFsm(StateMachine fsm) 
            => _fsm = fsm;

        protected void SetState<T>() where T : StateBase 
            => _fsm.SetState<T>();

        public virtual void Dispose()
        {
            
        }
    }
}