using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Abstract
{
    public interface IItem
    {
        bool CanEquip { get; }
        bool CanUse { get; }
        string Name { get; }

        Damage GetDamage(ICombatEntity entity);
        Damage ModifyDamage(Damage damage);
    }
}
