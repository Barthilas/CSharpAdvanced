using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.States
{
    internal class MainMenuState : IEngineState
    {
        private readonly Player _player;
        public MainMenuState(Player player)
        {
            _player = player;
        }
        public void Activate()
        {
            Console.Clear();
            Console.WriteLine("MAIN MENU - press enter for inventory, or esc to return to game");
        }

        public void Deactivate()
        {

        }

        public void Dispose()
        {

        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                Program.Engine.PopState(this);
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Program.Engine.PushState(new InventoryState(_player));
            }
        }
    }
}
