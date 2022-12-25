using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Model
{
    public class Combat
    {
        private readonly List<ICombatListener> _listeners;
        public Player Player { get; private set; }
        public ICombatEntity Entity { get; private set; }
        public Combat(Player player, ICombatEntity entity)
        {
            Player = player;
            Entity = entity;
            _listeners = new List<ICombatListener>();
        }

        public void UseItem(IItem item)
        {
            if (!item.CanUse)
            {
                _listeners.ForEach(f => f.DisplayMessage("Can not use " + item.Name));
                return;
            }

            PerformAction(item.GetDamage(Entity));

        }

        public void UseAbility(IAbility ability)
        {
            PerformAction(ability.GetDamage(Entity));
        }

        private void PerformAction(Damage damage)
        {
            _listeners.ForEach(f => f.DisplayMessage($"{Entity.Name} took {damage.Amount} damage from {damage.Text}"));

            Entity.TakeDamage(damage);
            if (Entity.Hp <= 0)
            {
                _listeners.ForEach(f => f.DisplayMessage($"{Entity.Name} died"));
                _listeners.ToList().ForEach(f => f.EndCombat());
            }

            damage = Entity.GetDamage(Player);
            _listeners.ForEach(f => f.DisplayMessage($"Player took {damage.Amount} damage from {damage.Text}"));
            Player.TakeDamage(damage);

            if (Player.Hp <= 0)
            {
                _listeners.ForEach(f => f.PlayerDied());
            }
        }

        public void AddListener(ICombatListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(ICombatListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
