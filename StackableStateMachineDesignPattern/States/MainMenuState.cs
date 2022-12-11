using StackableStateMachineDesignPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.States
{
    internal class MainMenuState : IEngineState
    {
        public void Activate()
        {
            Console.Clear();
            Console.WriteLine("MAIN MENU");
        }

        public void Deactivate()
        {
            
        }

        public void Dispose()
        {
            
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            if(key.Key == ConsoleKey.Escape)
            {
                Program.Engine.PopState(this);
            }
        }
    }
}
