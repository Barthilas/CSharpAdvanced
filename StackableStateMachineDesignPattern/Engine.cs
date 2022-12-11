using StackableStateMachineDesignPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern
{
    public class Engine
    {

        private readonly Stack<IEngineState> _states;
        public bool IsRunning { get; set; }
        public Engine()
        {
            IsRunning = true;
            _states= new Stack<IEngineState>();
        }
        public void Quit()
        {
            IsRunning= false;
        }

        public void PushState(IEngineState state)
        {
            if (_states.Count > 0)
                _states.Peek().Deactivate();

            _states.Push(state);
            state.Activate();
        }

        public void PopState(IEngineState state)
        {
            if (_states.Count == 0 || state != _states.Peek())
                throw new InvalidOperationException("No states left on stack, or you are popping a state you are not.");

            _states.Pop();
            state.Deactivate();
            state.Dispose();

            if (_states.Count > 0)
                _states.Peek().Activate();
        }

        public void SwitchState(IEngineState state)
        {
            if(_states.Count > 0)
            {
                var oldState = _states.Pop();
                oldState.Deactivate();
                oldState.Dispose();
            }

            _states.Push(state);
            state.Activate();
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            if(_states.Count > 0)
                _states.Peek().ProcessInput(key);
        }
    }
}
