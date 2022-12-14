using StackableStateMachineDesignPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Abstract
{
    public interface IZoneListener
    {
        void EntityMoved(Entity entity, Vector3 newPosition);
        void EntityRemoved(Entity entity);
        void EntityAdded(Entity entity);
    }
}
