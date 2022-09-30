using Patterns.ChainOfResponsobility.Handlers;
using Patterns.ChainOfResponsobility.Objects;

namespace Patterns.ChainOfResponsobility
{
    class Client
    {
        public static void Run(AbstractHandler handler)
        {
            List<object> objects = new()
            {
                new FirstObject() { DateTime = DateTime.Now },
                new SecondObject() { Id = Environment.ProcessId },
                new ThirdObject() { Name = Console.Title },
                new FourthObject(),
            };
            foreach (var entity in objects)
            {
                var result = handler.Handle(entity);

                if (result != null)
                {
                    Console.WriteLine($"{result}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{entity} not processed");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}
