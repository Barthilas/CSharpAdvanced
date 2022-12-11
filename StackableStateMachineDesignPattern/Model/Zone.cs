using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Model
{
    public class Zone
    {
        //better than list.
        private readonly HashSet<IZoneListener> _listeners;
        public Zone() { 
            _listeners = new HashSet<IZoneListener>();
        }

        public void MoveEntity()
        {
            _listeners.ForEach(a => a.EntityMoved());
        }

        public void AddListener(IZoneListener listener)
        {
            if (!_listeners.Add(listener))
                throw new ArgumentException();
        }

        public void RemoveListener(IZoneListener listener)
        {
            if (!_listeners.Remove(listener))
                throw new ArgumentException();
        }
    }
}
