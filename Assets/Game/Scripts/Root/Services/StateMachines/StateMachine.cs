using System;
using System.Collections.Generic;

namespace Game.Scripts.Root.Services.StateMachines
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();

        private IExitableState _currentState;
        
        public void RegisterState<T>(T state) where T : IExitableState
        {
            Type type = typeof(T);
            _states[type] = state;
        }

        public void Enter<T>() where T : class, IState
        {
            IState newState = ChangeState<T>();
            newState.Enter();
        }
        
        public void Enter<T, TPayLoad>(TPayLoad payLoad) where T : class, IPayloadState<TPayLoad>
        {
            T state = ChangeState<T>();
            state.Enter(payLoad);
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
        
        private T ChangeState<T>() where T : class, IExitableState
        {
            _currentState?.Exit();
      
            T state = GetState<T>();
            _currentState = state;
      
            return state;
        }
        
        private T GetState<T>() where T : class, IExitableState => 
            _states[typeof(T)] as T;
    }
}