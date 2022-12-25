using StackableStateMachineDesignPattern.Abstract;
using StackableStateMachineDesignPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.States
{
    public class CombatState : IEngineState, ICombatListener
    {
        private readonly Combat _combat;
        private int _selectedOption;
        private bool _combatEnded;
        public CombatState(Combat combat)
        {
            _combat = combat;
            _combat.AddListener(this);
        }
        public void Activate()
        {
            Render();
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            var abilityCount = _combat.Player.Abilities.Count();
            var itemCount = _combat.Player.Inventory.Where(i => i.CanUse).Count();
            var totalCount = abilityCount + itemCount;

            if (key.Key == ConsoleKey.W)
            {
                if (_selectedOption > 0)
                {
                    _selectedOption--;
                }
            }
            else if (key.Key == ConsoleKey.S)
            {
                if (_selectedOption < totalCount - 1)
                {
                    _selectedOption++;
                }
            }

            else if (key.Key == ConsoleKey.Spacebar)
            {
                if (_selectedOption < abilityCount) {
                    _combat.UseAbility(_combat.Player.Abilities.ElementAt(_selectedOption));
                }
                else
                {
                    _combat.UseItem(_combat.Player.Inventory.Where(i => i.CanUse).ElementAt(_selectedOption - abilityCount));
                }
            }

            if(!_combatEnded)
                Render();
        }

        public void Deactivate()
        {
        }

        public void DisplayMessage(string message)
        {
            RenderHeader();
            Console.WriteLine(message);
            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }

        public void Dispose()
        {
            _combat.RemoveListener(this);
        }

        public void EndCombat()
        {
            _combatEnded = true;
            Program.Engine.PopState(this);
        }
        private void ColorConsole(bool selected)
        {
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private void RenderHeader()
        {
            Console.Clear();
            Console.WriteLine("You (hp: {0}) vs {1} (hp: {2})", _combat.Player.Hp, _combat.Entity.Name, _combat.Entity.Hp);
            Console.WriteLine("------------------");
        }

        private void Render()
        {
            RenderHeader();
            var index = 0;
            foreach (var ability in _combat.Player.Abilities)
            {
                if (_selectedOption == index)
                    ColorConsole(true);

                Console.WriteLine(ability.Name);
                index++;
                ColorConsole(false);
            }

            Console.WriteLine("------------------");
            foreach (var item in _combat.Player.Inventory.Where(i => i.CanUse))
            {
                if (_selectedOption == index)
                    ColorConsole(true);

                Console.WriteLine(item.Name);
                index++;
                ColorConsole(false);
            }

        }

        public void PlayerDied()
        {
            Console.WriteLine("YOU DIED");
            Console.ReadKey();
            Program.Engine.Quit();
        }
    }
}
