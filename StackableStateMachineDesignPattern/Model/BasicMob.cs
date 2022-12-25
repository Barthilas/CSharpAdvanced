using StackableStateMachineDesignPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Model
{
    public class BasicMob : ICombatEntity
    {
        public string Name => "Basic Mob";

        public int Hp { get; private set; }

        public BasicMob()
        {
            Hp = 100;
        }

        public Damage GetDamage(Player player)
        {
            return new Damage("RAINBOW KICK!", 10);
        }

        public void TakeDamage(Damage damage)
        {
            Hp -= damage.Amount;
        }
    }
}
