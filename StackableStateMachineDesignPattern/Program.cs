using StackableStateMachineDesignPattern.Model;
using StackableStateMachineDesignPattern.Model.Components;
using StackableStateMachineDesignPattern.States;

namespace StackableStateMachineDesignPattern
{
    public static class Program
    {
        public static Engine Engine { get; private set; }
        static void Main(string[] args)
        {
            var random = new Random();
            //FIX: Console retaining weird colors after crashing.
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            const int ZoneWidth = 50;
            const int ZoneHeight = 30;
            Console.BufferHeight = Console.WindowHeight = ZoneHeight;
            Console.BufferWidth = Console.WindowWidth = ZoneWidth;

            Console.CursorVisible = false;

            var player = new Entity();
            var playerModel = new Player();
            playerModel.AddAbility(new Ability("Fireball", 10));
            playerModel.AddAbility(new Ability("FireStorm", 100));
            playerModel.AddItem(new Item("Axe", false, true, totalDamage: 30));
            playerModel.AddItem(new Item("Bow", false, true, totalDamage: 2));
            player.AddComponent(new SpriteComponent() { Sprite = '$' });
            player.AddComponent(new PlayerComponent(playerModel));
            player.Position = new Vector3(2, 2, 1);

            var tallGrass = new Entity();
            tallGrass.AddComponent(new SpriteComponent { Sprite = 'a' });
            tallGrass.Position = new Vector3(3, 3, 0);
            tallGrass.AddComponent(new CombatComponent(() => new Combat(playerModel, new BasicMob())));

            var ceilling = new Entity();
            ceilling.AddComponent(new SpriteComponent { Sprite = '@' });
            ceilling.Position = new Vector3(4, 4, 2);

            var wall = new Entity();
            wall.AddComponent(new ConstantEntranceComponent(false));
            wall.AddComponent(new SpriteComponent() { Sprite = '*' });
            wall.Position = new Vector3(5, 5, 0);

            var npc1 = new Entity();
            npc1.AddComponent(new DialogComponent(new Dialog
                (
                    new DialogScreen("Hey there!", nextScreens: new Dictionary<string, Abstract.IDialogScreen>
                    {

                        {"option 1", new DialogScreen("WHOOT", r => Console.WriteLine("ACTION 1")) },
                        {"option 2", new DialogScreen("STUFF", e => Console.WriteLine("ACTION 1"), new Dictionary<string, Abstract.IDialogScreen>
                        {
                            {"More stuff!", new DialogScreen("FINAL SCREEN") }
                        }) },
                    })
            )));
            npc1.AddComponent(new SpriteComponent() { Sprite = '!' });
            npc1.Position = new Vector3(1, 1, 0);

            var npc2 = new Entity();
            npc2.AddComponent(new DialogComponent(new Dialog
                (
                    new DialogScreen("Hey there! Have this item.", e => e.GetComponent<PlayerComponent>().Player.AddItem(new Item("Armor - " + random.Next(0, 100), true, false, -5)))
            )));
            npc2.AddComponent(new SpriteComponent() { Sprite = '?' });
            npc2.Position = new Vector3(2, 1, 0);

            var zone1 = new Zone("Zone 1", new Vector3(ZoneWidth, ZoneHeight, 3));
            zone1.AddEntity(player);
            zone1.AddEntity(tallGrass);
            zone1.AddEntity(ceilling);
            zone1.AddEntity(wall);
            zone1.AddEntity(npc1);
            zone1.AddEntity(npc2);

            Engine = new Engine();
            Engine.PushState(new ZoneState(player, zone1));

            while (Engine.IsRunning)
            {
                Engine.ProcessInput(Console.ReadKey(true));
            }
        }
    }
}