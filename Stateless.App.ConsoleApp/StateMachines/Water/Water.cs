using Stateless.Graph;
using System;
using System.Threading.Tasks;

namespace Stateless.App.ConsoleApp.StateMachines.Water
{
    public sealed class Water
    {
        private StateMachine<WaterState, WaterTrigger> _machine;

        public Water()
        {
            var water = new StateMachine<WaterState, WaterTrigger>(
                () => State,
                s => State = s,
                FiringMode.Queued
            );

            water.Configure(WaterState.Solid)
                .OnEntryAsync(OnEntrySolidAsync)
                .OnExitAsync(OnExitSolidAsync)                
                .Permit(WaterTrigger.OnMelted, WaterState.Liquid)
                .Permit(WaterTrigger.OnSublimation, WaterState.Gas);

            water.Configure(WaterState.Liquid)
                .OnEntryAsync(OnEntryLiquidAsync)
                .OnExitAsync(OnExitLiquidAsync)
                .Permit(WaterTrigger.OnFroze, WaterState.Solid)
                .Permit(WaterTrigger.OnVaporized, WaterState.Gas);

            water.Configure(WaterState.Gas)
                .OnEntryAsync(OnEntryGasAsync)
                .OnExitAsync(OnExitGasAsync)
                .Permit(WaterTrigger.OnCondensed, WaterState.Liquid)
                .Permit(WaterTrigger.OnDeposition, WaterState.Solid);


            water.OnTransitionedAsync(OnTransitionAsync);
            _machine = water;
        }            

        public WaterState State { get; private set; } = WaterState.Liquid;

        public string ToDotGraph()
        {
            return UmlDotGraph.Format(_machine.GetInfo());
        }

        public async Task LoremIpsumAsync()
        {
            await VaporizeAsync();
            await DepositionAsync();
            await MeltAsync();
            await FreezeAsync();
            await SublimateAsync();
            await CondensateAsync();
        }

        public async Task VaporizeAsync()
        {
            await _machine.FireAsync(WaterTrigger.OnVaporized);
        }

        public async Task DepositionAsync()
        {
            await _machine.FireAsync(WaterTrigger.OnDeposition);
        }

        public async Task MeltAsync()
        {
            await _machine.FireAsync(WaterTrigger.OnMelted);
        }

        public async Task FreezeAsync()
        {
            await _machine.FireAsync(WaterTrigger.OnFroze);
        }

        public async Task SublimateAsync()
        {
            await _machine.FireAsync(WaterTrigger.OnSublimation);
        }

        public async Task CondensateAsync()
        {
            await _machine.FireAsync(WaterTrigger.OnCondensed);
        }

        private async Task OnTransitionAsync(StateMachine<WaterState, WaterTrigger>.Transition transition)
        {
            Console.WriteLine($"Transition {transition.Trigger}: {transition.Source}=>{transition.Destination}");
            await Task.Delay(1000);
        }

        private async Task OnEntrySolidAsync()
        {
            Console.WriteLine("Solidifying...");
            await Task.Delay(1000);
        }

        private async Task OnEntryLiquidAsync()
        {
            Console.WriteLine("Liquefying...");
            await Task.Delay(1000);
        }

        private async Task OnEntryGasAsync()
        {
            Console.WriteLine("Gasification...");
            await Task.Delay(1000);
        }

        private async Task OnExitSolidAsync()
        {
            Console.WriteLine("Solid is gone...");
            await Task.Delay(1000);
        }

        private async Task OnExitLiquidAsync()
        {
            Console.WriteLine("Liquid is gone...");
            await Task.Delay(1000);
        }

        private async Task OnExitGasAsync()
        {
            Console.WriteLine("Gas is gone...");
            await Task.Delay(1000);
        }
    }
}
