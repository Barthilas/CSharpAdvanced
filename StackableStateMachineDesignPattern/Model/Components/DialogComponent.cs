using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Model.Components
{
    public class DialogComponent : Component, IEntityEntranceComponent
    {
        private readonly IDialog _dialog;

        public DialogComponent(IDialog dialog) {
            _dialog = dialog;
        }
        public bool CanEnter(Entity entity)
        {
            return true;
        }

        public void Enter(Entity entity)
        {
            Program.Engine.PushState(new DialogState(entity, _dialog));
        }
    }
}
