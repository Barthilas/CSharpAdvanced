using StackableStateMachineDesignPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.States
{
    internal class ZoneRenderer : IZoneListener
    {
        public void EntityMoved()
        {
            Console.WriteLine("ENTITY MOVED");
        }
    }
}
