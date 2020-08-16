using Stateless.App.ConsoleApp.StateMachines.Water;
using System;
using System.Threading.Tasks;

namespace Stateless.App.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine(UmlDotGraph.Format(new Water().StateMachine.GetInfo()));
            //Console.Read();
            //var interactive = new InteractiveStateMachineConsole<WaterState, WaterTrigger>(new Water().StateMachine);
            //interactive.InteractAsync().Wait();

            await WaterAsync();

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }

        static async Task WaterAsync()
        {
            var water = new Water();
            await water.VaporizeAsync();
            await water.DepositionAsync();
            await water.MeltAsync();
            await water.FreezeAsync();
            await water.SublimateAsync();
            await water.CondensateAsync();
            Console.WriteLine(water.ToDotGraph());
        }
    }
}
