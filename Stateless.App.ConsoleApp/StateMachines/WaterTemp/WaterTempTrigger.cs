using System;

namespace Stateless.App.ConsoleApp.StateMachines.WaterHeat
{
    public abstract class WaterTempTrigger
    {
        private WaterTempTrigger(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public sealed class OnHeatUp : WaterTempTrigger
        {
            public OnHeatUp(double value) : base(value > 0 ? value : throw new ArgumentException("Must be positive", nameof(value)))
            {
            }
        }

        public sealed class OnCoolDown : WaterTempTrigger
        {
            public OnCoolDown(double value) : base(value < 0 ? value : throw new ArgumentException("Must be negative", nameof(value)))
            {
            }
        }
    }
}
