using StackableStateMachineDesignPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern
{
    internal class Engine
    {
        public bool IsRunning { get; set; }
        public Engine()
        {

        }
        public void Quit()
        {

        }

        public void PushState(IEngineState state)
        {

        }

        public void PopState(IEngineState state)
        {

        }

        public void SwitchState(IEngineState state)
        {

        }

        public void ProcessInput(ConsoleKeyInfo key)
        {

        }
    }
}
