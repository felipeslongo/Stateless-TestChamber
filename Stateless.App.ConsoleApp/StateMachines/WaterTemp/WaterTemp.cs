using Stateless.App.ConsoleApp.StateMachines.WaterHeat;

namespace Stateless.App.ConsoleApp.StateMachines.WaterTemp
{
    class WaterTemp
    {
        public WaterTemp()
        {
            var water = new StateMachine<WaterTempState, WaterTempTrigger>(
                () => State,
                s => State = s,
                FiringMode.Queued
            );            
        }

        public StateMachine<WaterTempState, WaterTempTrigger> StateMachine { get; }

        public WaterTempState State { get; private set; } = new WaterTempState.Liquid(20);
    }
}
