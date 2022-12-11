using StackableStateMachineDesignPattern.Model;
using StackableStateMachineDesignPattern.States;

namespace StackableStateMachineDesignPattern
{
    public static class Program
    {
        public static Engine Engine { get; private set; }
        static void Main(string[] args)
        {
            var zone1 = new Zone();
            Engine = new Engine();
            Engine.PushState(new ZoneState(zone1));

            while(Engine.IsRunning)
            {
                Engine.ProcessInput(Console.ReadKey(true));
            }
        }
    }
}