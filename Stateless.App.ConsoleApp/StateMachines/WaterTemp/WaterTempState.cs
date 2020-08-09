using System;

namespace Stateless.App.ConsoleApp.StateMachines.WaterTemp
{
    public abstract class WaterTempState
    {
        private WaterTempState(double temperature)
        {
            if (temperature < MinTemp)
                throw new ArgumentException($"Temperature {temperature} must be above {MinTemp}", nameof(temperature));
            if (temperature > MaxTemp)
                throw new ArgumentException($"Temperature {temperature} must be below {MaxTemp}", nameof(temperature));

            Temperature = temperature;
        }

        public double Temperature { get; }

        public abstract double MaxTemp { get; }

        public abstract double MinTemp { get; }

        public sealed class Solid : WaterTempState
        {
            public Solid(double temperature) : base(temperature)
            {
            }

            public override double MaxTemp => -1;

            public override double MinTemp => double.MinValue;
        }

        public sealed class Liquid : WaterTempState
        {
            public Liquid(double temperature) : base(temperature)
            {
            }

            public override double MaxTemp => 100;

            public override double MinTemp => 0;
        }

        public sealed class Gas : WaterTempState
        {
            public Gas(double temperature) : base(temperature)
            {
            }

            public override double MaxTemp => double.MaxValue;

            public override double MinTemp => 101;
        }
    }
}
