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
            const int ZoneWidth = 50;
            const int ZoneHeight = 30;
            Console.BufferHeight = Console.WindowHeight = ZoneHeight;
            Console.BufferWidth = Console.WindowWidth = ZoneWidth;

            Console.CursorVisible = false;

            var player = new Entity();
            player.AddComponent(new SpriteComponent() { Sprite = '$' });
            player.Position = new Vector3(2, 2, 1);

            var tallGrass = new Entity();
            tallGrass.AddComponent(new SpriteComponent { Sprite = 'a' });
            tallGrass.Position = new Vector3(3, 3, 0);

            var ceilling = new Entity();
            ceilling.AddComponent(new SpriteComponent { Sprite = '@' });
            ceilling.Position = new Vector3(4, 4, 2);

            var zone1 = new Zone("Zone 1", new Vector3(ZoneWidth, ZoneHeight, 3));
            zone1.AddEntity(player);
            zone1.AddEntity(tallGrass);
            zone1.AddEntity(ceilling);

            Engine = new Engine();
            Engine.PushState(new ZoneState(player, zone1));

            while (Engine.IsRunning)
            {
                Engine.ProcessInput(Console.ReadKey(true));
            }
        }
    }
}