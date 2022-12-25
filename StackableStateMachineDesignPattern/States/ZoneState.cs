using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.Model;
using StackableStateMachineDesignPattern.Model.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.States
{
    internal class ZoneState : IEngineState
    {
        private readonly Entity _player;
        private readonly Zone _zone;
        private readonly ZoneRenderer _renderer;
        public ZoneState(Entity player, Zone zone)
        {
            _player = player;
            _zone= zone;
            _renderer= new ZoneRenderer(_zone);

            _zone.AddListener(_renderer);
        }
        public void Activate()
        {
            _renderer.IsActive = true;
            _renderer.RenderAll();
        }

        public void Deactivate()
        {
            _renderer.IsActive = false;
        }
         
        public void Dispose()
        {
            _zone.RemoveListener(_renderer);
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            var pos = _player.Position;
            if (key.Key == ConsoleKey.Escape)
            {
                Program.Engine.PushState(new MainMenuState(_player.GetComponent<PlayerComponent>().Player));
            }
            else if (key.Key == ConsoleKey.W)
                _zone.MoveEntity(_player, new Vector3(pos.X, pos.Y - 1, pos.Z));
            else if (key.Key == ConsoleKey.A)
                _zone.MoveEntity(_player, new Vector3(pos.X - 1, pos.Y, pos.Z));
            else if (key.Key == ConsoleKey.S)
                _zone.MoveEntity(_player, new Vector3(pos.X, pos.Y + 1, pos.Z));
            else if (key.Key == ConsoleKey.D)
                _zone.MoveEntity(_player, new Vector3(pos.X + 1, pos.Y, pos.Z));
        }
    }
}
