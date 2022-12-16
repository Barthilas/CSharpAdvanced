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
        private readonly Entity[,,] _entities;
        public string Name { get; private set; }
        public Vector3 Size { get; private set; }
        //better than list.
        private readonly HashSet<IZoneListener> _listeners;


        public IEnumerable<Entity> Entities
        {
            get
            {
                for (var x = 0; x < Size.X; x++)
                    for (var y = 0; y < Size.Y; y++)
                        for (var z = 0; z < Size.Z; z++)
                        {
                            var entity = _entities[x, y, z];
                            if (entity == null) continue;
                            yield return entity;
                        }
            }
        }
        public Zone(string name, Vector3 size)
        {
            _listeners = new HashSet<IZoneListener>();
            Size = size;
            Name = name;
            _entities = new Entity[size.X, size.Y, size.Z];
        }

        public void MoveEntity(Entity entity, Vector3 newPosition)
        {
            if (CanMove(newPosition))
                return;
                _listeners.ForEach(l => l.EntityMoved(entity, newPosition));
            _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;
            entity.Position = newPosition;
            _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = entity;
        }

        public bool CanMove(Vector3 position)
        {
            return position.X < 0 || position.X >= Size.X
                || position.Y < 0 || position.Y >= Size.Y
                || position.Z < 0 || position.Z >= Size.Z;
        }

        public void AddEntity(Entity entity)
        {
            _listeners.ForEach(l => l.EntityAdded(entity));
            _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = entity;
        }

        public void RemoveEntity(Entity entity)
        {
            _listeners.ForEach(l => l.EntityRemoved(entity));
            var oldEntity = _entities[entity.Position.X, entity.Position.Y, entity.Position.Z];
            if (oldEntity != entity)
                throw new InvalidOperationException("Entity position is out of sync");

            _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;
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
