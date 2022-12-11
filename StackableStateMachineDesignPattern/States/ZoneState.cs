using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.States
{
    internal class ZoneState : IEngineState
    {
        private readonly Zone _zone;
        private readonly ZoneRenderer _renderer;
        public ZoneState(Zone zone)
        {
            _zone= zone;
            _renderer= new ZoneRenderer();

            _zone.AddListener(_renderer);
        }
        public void Activate()
        {
            Console.Clear();
            Console.WriteLine("ZONE");
        }

        public void Deactivate()
        {
            
        }
         
        public void Dispose()
        {
            _zone.RemoveListener(_renderer);
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                Program.Engine.PushState(new MainMenuState());
            }
            else if (key.Key == ConsoleKey.W)
                _zone.MoveEntity();
            else if (key.Key == ConsoleKey.A)
                _zone.MoveEntity();
            else if (key.Key == ConsoleKey.S)
                _zone.MoveEntity();
            else if (key.Key == ConsoleKey.D)
                _zone.MoveEntity();
        }
    }
}
