using System;
using System.Threading.Tasks;

namespace Stateless.App.ConsoleApp
{
    public sealed class InteractiveStateMachineConsole<TState, TTrigger>
    {
        const int ExitInput = 0;

        public InteractiveStateMachineConsole(StateMachine<TState, TTrigger> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public async Task InteractAsync()
        {           
            while(true)
            {
                Console.WriteLine($"Current State: {StateMachine.State}");
                var userInput = await GetInputAsync();
                if (userInput == ExitInput)
                    break;

                //await FireAsync(userInput);
            }
        }      

        public StateMachine<TState, TTrigger> StateMachine { get; }

        private Task<int> GetInputAsync()
        {
            Console.WriteLine($"{ExitInput}:Exit");
            foreach (var trigger in StateMachine.PermittedTriggers)
            {
                Console.WriteLine($"{trigger.GetHashCode()}:{trigger}");
            }

            Console.Write("Please Enter a State Trigger: ");
            var input = Console.ReadLine();
            return Task.FromResult(Convert.ToInt32(input));
        }

        private async Task FireAsync(TTrigger userInput)
        {
            await StateMachine.FireAsync(userInput);
        }
    }
}
